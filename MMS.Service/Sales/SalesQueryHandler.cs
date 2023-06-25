using MediatR;
using MMS.Core.Repositories;
using MMS.Service.Sales.Queries;
using MMS.Service.Sales.Responses;

namespace MMS.Service.Sales
{
    public class SalesQueryHandler : IRequestHandler<GetSalesListQuery, List<SalesListResponse>>
    {
        private readonly IRepository<Sale> _saleRepository;

        public SalesQueryHandler(IRepository<Sale> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<SalesListResponse>> Handle(GetSalesListQuery request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _saleRepository.GetMappedListAsync<SalesListResponse, GetSalesListQuery>(
                SalesListResponse.Configuration, request, cancellationToken: cancellationToken);
        }
    }
}
