using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MMS.Core.Domain;
using MMS.Core.Extensions;
using MMS.Core.Filtering;
using MMS.Core.Pagination;
using MMS.Core.Queries;

namespace MMS.Core.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private DbSet<TEntity> Table { get; }

    private bool _disposed;
    private readonly DbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly RepositoryOptions _repositoryOptions;

    public Repository(IServiceProvider serviceProvider, RepositoryOptions repositoryOptions)
    {
        _serviceProvider = serviceProvider;
        _repositoryOptions = repositoryOptions;
        _context = GetDbContext();
        Table = _context.Set<TEntity>();
    }

    #region Disposable

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    } 

    #endregion

    private DbContext GetDbContext()
    {
        return _serviceProvider.GetRequiredService(_repositoryOptions.DbContextType ??
                                                   throw new ArgumentException(
                                                       "ContextType was not set in RepositoryOptions!")) as
            DbContext ?? throw new ArgumentException("DbContext was not found!");
    }

    protected IQueryable<TEntity> GetQuery(
        in IQueryable<TEntity> source,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        var query = source;

        query = predicate != null ? query.Where(predicate) : query;

        orderBy ??= q => q.OrderBy(x => x.Id);

        query = orderBy(query);

        return query;
    }

    protected IQueryable<TEntity> GetQuery<TFilter>(
        in IQueryable<TEntity> source,
        TFilter? filter = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null) where TFilter : IRequest
    {
        var query = source;

        var filterService = filter != null
            ? _serviceProvider.GetService<IFilterService<TEntity, TFilter>>()
            : null;

        query = filterService != null ? filterService.FilterBy(query, filter!) : query;

        orderBy ??= q => q.OrderBy(x => x.Id);

        query = orderBy(query);

        return query;
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        return await GetQuery(in query, predicate, orderBy).ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync<TFilter>(
        TFilter? filter = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TFilter : IRequest
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        return await GetQuery(query, filter, orderBy).ToListAsync(cancellationToken);
    }

    public async Task<PagedList<TEntity>> GetPagedListAsync(PagedRequest pagedRequest,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        query = GetQuery(query, predicate, orderBy);

        return await PagedList<TEntity>.CreateAsync(query, pagedRequest.PageNumber, pagedRequest.PageSize, cancellationToken);
    }

    public async Task<PagedList<TEntity>> GetPagedListAsync<TFilter>(TFilter? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TFilter : PagedRequest
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        filter ??= new PagedRequest() as TFilter;

        query = GetQuery(query, filter, orderBy);

        return await PagedList<TEntity>.CreateAsync(query, filter!.PageNumber, filter.PageSize, cancellationToken);
    }

    public async Task<List<TTarget>> GetMappedListAsync<TTarget>(
        IConfigurationProvider mapperConfiguration,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TTarget : class
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        var resultQuery= GetQuery(query, predicate, orderBy).ProjectTo<TTarget>(mapperConfiguration);

        return await resultQuery.ToListAsync(cancellationToken);
    }

    public async Task<List<TTarget>> GetMappedListAsync<TTarget, TFilter>(
        IConfigurationProvider mapperConfiguration,
        TFilter? filter = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TTarget : class where TFilter : IRequest
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        var resultQuery = GetQuery(query, filter, orderBy).ProjectTo<TTarget>(mapperConfiguration);

        return await resultQuery.ToListAsync(cancellationToken);
    }

    public async Task<PagedList<TTarget>> GetMappedPagedList<TTarget>(
        PagedRequest pagedRequest,
        IConfigurationProvider mapperConfiguration,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TTarget : class
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        var resultQuery = GetQuery(query, predicate, orderBy).ProjectTo<TTarget>(mapperConfiguration);

        return await PagedList<TTarget>.CreateAsync(resultQuery, pagedRequest.PageNumber, pagedRequest.PageSize, cancellationToken);
    }

    public async Task<PagedList<TTarget>> GetMappedPagedList<TTarget, TFilter>(IConfigurationProvider mapperConfiguration,
        TFilter? filter = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
        where TTarget : class
        where TFilter : PagedRequest
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        filter ??= new PagedRequest() as TFilter;

        var resultQuery = GetQuery(query, filter, orderBy).ProjectTo<TTarget>(mapperConfiguration);

        return await PagedList<TTarget>.CreateAsync(resultQuery, filter!.PageNumber, filter.PageSize, cancellationToken);
    }

    public async Task<TEntity?> GetAsync(int id, string[]? relatedProperties = null, CancellationToken cancellationToken = default)
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, string[]? relatedProperties = null, CancellationToken cancellationToken = default)
    {
        if(predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsNoTracking();

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<TTarget?> GetMappedAsync<TTarget>(int id, IConfigurationProvider mapperConfig,
        string[]? relatedProperties = null, CancellationToken cancellationToken = default)
    {
        var query = Table
            .Where(x => x.Id == id)
            .ApplyIncludes(relatedProperties)
            .AsQueryable()
            .AsNoTracking()
            .ProjectTo<TTarget>(mapperConfig);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TTarget> GetMappedAsync<TTarget>(Expression<Func<TEntity, bool>> predicate, IConfigurationProvider mapperConfig, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
    {
        var query = Table
            .Where(predicate)
            .ApplyIncludes(relatedProperties)
            .AsQueryable()
            .AsNoTracking()
            .ProjectTo<TTarget>(mapperConfig);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var query = Table.AsQueryable().AsNoTracking();

        query = predicate != null ? query.Where(predicate) : query;

        return await query.LongCountAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var query = Table.AsQueryable().AsNoTracking();

        query = predicate != null ? query.Where(predicate) : query;

        return query.AnyAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListForUpdateAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsTracking();

        return await GetQuery(query, predicate, orderBy).ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListForUpdateAsync<TFilter>(TFilter? filter = default, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TFilter : IRequest
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsTracking();

        return await GetQuery(query, filter, orderBy).ToListAsync(cancellationToken);

    }

    public async Task<TEntity?> GetForUpdateAsync(int id, string[]? relatedProperties = null, CancellationToken cancellationToken = default)
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<TEntity?> GetForUpdateAsync(Expression<Func<TEntity, bool>> predicate, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
    {
        var query = Table.ApplyIncludes(relatedProperties).AsQueryable().AsTracking();

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);

    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if(entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        Table.Add(entity);

        if (_context.Entry(entity).IsKeySet)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        await SaveChanges(cancellationToken);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if(entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await SaveChanges(cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(int id, bool shouldPersist = false, CancellationToken cancellationToken = default)
    {
        var entity = await GetForUpdateAsync(id, null, cancellationToken);

        if (entity != null)
        {
            if (shouldPersist)
            {
                var aggregate = entity as AggregateRoot;
                if (aggregate == null)
                {
                    throw new ArgumentException("entity can not be converted to aggregate!");
                }

                aggregate.Delete();

                await UpdateAsync((TEntity)(Entity)aggregate, cancellationToken);
            }
            else
            {
                Table.Remove(entity);
                await SaveChanges(cancellationToken);
            }
        }
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool shouldPersist = false, CancellationToken cancellationToken = default)
    {
        var entity = await GetForUpdateAsync(predicate, null, cancellationToken);

        if (entity != null)
        {
            if (shouldPersist)
            {
                var aggregate = entity as AggregateRoot;
                if (aggregate == null)
                {
                    throw new ArgumentException("entity can not be converted to aggregate!");
                }

                aggregate.Delete();

                await UpdateAsync((TEntity)(Entity)aggregate, cancellationToken);
            }
            else
            {
                Table.Remove(entity);
                await SaveChanges(cancellationToken);
            }
        }
    }

    public async Task RestoreAsync(int aggregateId, CancellationToken cancellationToken = default)
    {
        var entity = await GetForUpdateAsync(aggregateId, cancellationToken: cancellationToken);

        var aggregate = entity as AggregateRoot;

        if(aggregate == null)
        {
            throw new ArgumentException($"entity of type {typeof(TEntity)} can not be converted to {typeof(AggregateRoot)}!");
        }

        aggregate.Restore();

        await UpdateAsync((TEntity)(Entity)aggregate, cancellationToken);
    }

    public async Task RestoreAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await GetForUpdateAsync(predicate, cancellationToken: cancellationToken);

        var aggregate = entity as AggregateRoot;

        if (aggregate == null)
        {
            throw new ArgumentException($"entity of type {typeof(TEntity)} can not be converted to {typeof(AggregateRoot)}!");
        }

        aggregate.Restore();

        await UpdateAsync((TEntity)(Entity)aggregate, cancellationToken);
    }

    private Task SaveChanges(CancellationToken cancellationToken = default)
    {
        return _repositoryOptions.SaveChangesStrategy switch
        {
            SaveChangesStrategy.PerOperation => _context.SaveChangesAsync(cancellationToken),
            SaveChangesStrategy.PerUnitOfWork when _context.Database.CurrentTransaction == null => _context
                .SaveChangesAsync(cancellationToken),
            _ => Task.CompletedTask
        };
    }
}