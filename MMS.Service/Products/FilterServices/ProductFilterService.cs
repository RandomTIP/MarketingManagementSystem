using Microsoft.EntityFrameworkCore;
using MMS.Core.Filtering;
using MMS.Service.Products.Queries;

namespace MMS.Service.Products.FilterServices
{
    public class ProductFilterService : IFilterService<Product, GetProductListQuery>
    {
        public IQueryable<Product> FilterBy(IQueryable<Product> source, GetProductListQuery filter)
        {
            if (!string.IsNullOrEmpty(filter.Code))
            {
                source = source.Where(x => x.Code == filter.Code);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                source = source.Where(x => x.Name == filter.Name);
            }

            if (filter.UnitPrice.HasValue)
            {
                source = source.Where(x => x.UnitPrice == filter.UnitPrice);
            }

            return source;
        }

        public async Task<IEnumerable<Product>> FilterByAsync(IQueryable<Product> source, GetProductListQuery filter, CancellationToken cancellationToken = default)
        {
            return await FilterBy(source, filter).ToListAsync(cancellationToken);
        }

        public IEnumerable<Product> FilterEnumerableBy(IEnumerable<Product> source, GetProductListQuery filter)
        {
            if (!string.IsNullOrEmpty(filter.Code))
            {
                source = source.Where(x => x.Code == filter.Code);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                source = source.Where(x => x.Name == filter.Name);
            }

            if (filter.UnitPrice.HasValue)
            {
                source = source.Where(x => x.UnitPrice == filter.UnitPrice);
            }

            return source;
        }
    }
}
