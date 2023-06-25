using MediatR;
using MMS.Core.Repositories;
using MMS.Service.Distributors.Queries;
using MMS.Service.Distributors.Responses;

namespace MMS.Service.Distributors
{
    public class DistributorQueryHandler : IRequestHandler<GetDistributorListRequest, List<DistributorListResponse>>
    {
        private readonly IRepository<Distributor> _distributorRepository;

        public DistributorQueryHandler(IRepository<Distributor> repository)
        {
            _distributorRepository = repository;
        }

        public async Task<List<DistributorListResponse>> Handle(GetDistributorListRequest request, CancellationToken cancellationToken)
        {
            return await _distributorRepository.GetMappedListAsync<DistributorListResponse>(
                DistributorListResponse.ConfigurationProvider,
                relatedProperties: new[]
                {
                    "GenderType", "AddressInformation", "ContactInformation", 
                    "IdentityDocument", "RecommendationAuthorDistributor"
                },
                cancellationToken: cancellationToken);
        }
    }
}
