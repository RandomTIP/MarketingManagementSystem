using MediatR;
using MMS.Core.Repositories;
using MMS.Service.Common.POCO;
using MMS.Service.Products.Commands;

namespace MMS.Service.Products
{
    public class ProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IRepository<Product> _repository;

        public ProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), $"Command can not be null!({nameof(CreateProductCommand)})");
            }

            request.Validate();

            var codes =
                await _repository.GetMappedListAsync<PreviousCodeModel>(PreviousCodeModel.ProductConfiguration,
                    cancellationToken: cancellationToken);

            var product = new Product(request, codes.LastOrDefault()?.Code);

            product = await _repository.InsertAsync(product, cancellationToken);

            return product.Id;
        }
    }
}
