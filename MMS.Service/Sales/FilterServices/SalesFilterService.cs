using Microsoft.EntityFrameworkCore;
using MMS.Core.Filtering;
using MMS.Service.Sales.Queries;

namespace MMS.Service.Sales.FilterServices
{
    public class SalesFilterService : IFilterService<Sale, GetSalesListQuery>
    {
        public IQueryable<Sale> FilterBy(IQueryable<Sale> source, GetSalesListQuery filter)
        {
            if (filter.DistributorId.HasValue)
            {
                source = source.Where(x => x.DistributorId == filter.DistributorId);
            }

            if (filter.SaleDate.HasValue)
            {
                source = source.Where(x => x.SaleDate == filter.SaleDate);
            }

            if (filter.ProductId.HasValue)
            {
                source = source.Where(x => x.ProductId == filter.ProductId);
            }

            return source;
        }

        public async Task<IEnumerable<Sale>> FilterByAsync(IQueryable<Sale> source, GetSalesListQuery filter, CancellationToken cancellationToken = default)
        {
            return await FilterBy(source, filter).ToListAsync(cancellationToken);
        }

        public IEnumerable<Sale> FilterEnumerableBy(IEnumerable<Sale> source, GetSalesListQuery filter)
        {
            if (filter.DistributorId.HasValue)
            {
                source = source.Where(x => x.DistributorId == filter.DistributorId);
            }

            if (filter.SaleDate.HasValue)
            {
                source = source.Where(x => x.SaleDate == filter.SaleDate);
            }

            if (filter.ProductId.HasValue)
            {
                source = source.Where(x => x.ProductId == filter.ProductId);
            }

            return source;
        }
    }
}
