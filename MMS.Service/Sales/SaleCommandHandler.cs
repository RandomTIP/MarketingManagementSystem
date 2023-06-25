using MediatR;
using MMS.Core.Repositories;
using MMS.Service.Common.POCO;
using MMS.Service.Sales.Command;

namespace MMS.Service.Sales
{
    public class SaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly IRepository<Sale> _saleRepository;

        public SaleCommandHandler(IRepository<Sale> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request), "Command can not be null!");
            }

            request.Validate();

            var codes = await _saleRepository.GetMappedListAsync<PreviousCodeModel>(PreviousCodeModel
                .SaleConfiguration, cancellationToken: cancellationToken);

            var sale = new Sale(request, codes.LastOrDefault()?.Code);

            sale = await _saleRepository.InsertAsync(sale, cancellationToken);

            return sale.Id;
        }
    }
}
