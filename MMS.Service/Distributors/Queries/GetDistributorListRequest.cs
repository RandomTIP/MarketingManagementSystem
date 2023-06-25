using MediatR;
using MMS.Service.Distributors.Responses;

namespace MMS.Service.Distributors.Queries
{
    public class GetDistributorListRequest : IRequest<List<DistributorListResponse>>
    {
    }
}