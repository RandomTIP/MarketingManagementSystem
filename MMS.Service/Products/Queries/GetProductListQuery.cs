using MediatR;
using MMS.Core.Pagination;
using MMS.Core.Queries;
using MMS.Service.Products.Responses;

namespace MMS.Service.Products.Queries
{
    public class GetProductListQuery : PagedRequest, IRequest<PagedList<ProductListResponse>>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
