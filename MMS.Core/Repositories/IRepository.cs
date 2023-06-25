using System.Linq.Expressions;
using AutoMapper;
using MMS.Core.Domain;
using MMS.Core.Pagination;
using MMS.Core.Queries;

namespace MMS.Core.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync<TFilter>(TFilter? filter = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TFilter : IRequest;

    Task<PagedList<TEntity>> GetPagedListAsync(PagedRequest pagedRequest, Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<PagedList<TEntity>> GetPagedListAsync<TFilter>(TFilter? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TFilter : PagedRequest;

    Task<List<TTarget>> GetMappedListAsync<TTarget>(IConfigurationProvider mapperConfiguration,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
        where TTarget : class;

    Task<List<TTarget>> GetMappedListAsync<TTarget, TFilter>(IConfigurationProvider mapperConfiguration,
        TFilter? filter = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
        where TFilter : IRequest
        where TTarget : class;

    Task<PagedList<TTarget>> GetMappedPagedList<TTarget>(
        PagedRequest pagedRequest,
        IConfigurationProvider mapperConfiguration,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
        where TTarget : class;

    Task<PagedList<TTarget>> GetMappedPagedList<TTarget, TFilter>(
        IConfigurationProvider mapperConfiguration,
        TFilter? filter = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default)
        where TFilter : PagedRequest
        where TTarget : class;

    Task<TEntity?> GetAsync(int id, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<TTarget?> GetMappedAsync<TTarget>(int id, IConfigurationProvider mapperConfig,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<TTarget> GetMappedAsync<TTarget>(Expression<Func<TEntity, bool>> predicate, IConfigurationProvider mapperConfig, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListForUpdateAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListForUpdateAsync<TFilter>(
        TFilter? filter = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string[]? relatedProperties = null,
        CancellationToken cancellationToken = default) where TFilter : IRequest;

    Task<TEntity?> GetForUpdateAsync(int id, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetForUpdateAsync(Expression<Func<TEntity, bool>> predicate, string[]? relatedProperties = null,
        CancellationToken cancellationToken = default);

    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, bool shouldPersist = false, CancellationToken cancellationToken = default);

    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool shouldPersist = false, CancellationToken cancellationToken = default);

    Task RestoreAsync(int aggregateId, CancellationToken cancellationToken = default);

    Task RestoreAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}