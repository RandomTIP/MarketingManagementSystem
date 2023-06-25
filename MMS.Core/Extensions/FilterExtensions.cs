using MMS.Core.Domain;
using MMS.Core.Filtering;
using MMS.Core.Queries;

namespace MMS.Core.Extensions
{
    public static class FilterExtensions
    {
        public static IQueryable<TEntity> FilterBy<TEntity>(this IQueryable<TEntity> source, IFilterService<TEntity, IRequest> filterService, IRequest filter)
            where TEntity : Entity
        {
            return filterService.FilterBy(source, filter);
        }

        public static async Task<IEnumerable<TEntity>> FilterByAsync<TEntity>(this IQueryable<TEntity> source, IFilterService<TEntity, IRequest> filter, IRequest request)
            where TEntity : Entity
        {
            return await filter.FilterByAsync(source, request);
        }

        public static IEnumerable<TEntity> FilterEnumerableBy<TEntity>(this IEnumerable<TEntity> source,
            IFilterService<TEntity, IRequest> service, IRequest filter)
            where TEntity : Entity
        {
            return service.FilterEnumerableBy(source, filter);
        }
    }
}
