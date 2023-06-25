using MediatR;
using MMS.Service.Sales.Responses;
using IRequest = MMS.Core.Queries.IRequest;

namespace MMS.Service.Sales.Queries
{
    public class GetSalesListQuery : IRequest, IRequest<List<SalesListResponse>>
    {
        public int? DistributorId { get; set; }
        public DateTime? SaleDate { get; set; }
        public int? ProductId { get; set; }
    }
}
