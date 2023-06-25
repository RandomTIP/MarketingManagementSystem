using MMS.Core.Domain;
using MMS.Core.Queries;

namespace MMS.Core.Filtering;

public interface IFilterService<TEntity, in TFilter>
    where TEntity : Entity
    where TFilter : IRequest
{
    IQueryable<TEntity> FilterBy(IQueryable<TEntity> source, TFilter filter);

    /// <summary>
    /// This will Execute a Query!
    /// </summary>
    /// <param name="source"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> FilterByAsync(IQueryable<TEntity> source, TFilter filter, CancellationToken cancellationToken = default);

    IEnumerable<TEntity> FilterEnumerableBy(IEnumerable<TEntity> source, TFilter filter);
}