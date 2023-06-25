using MediatR;
using MMS.Core.Pagination;
using MMS.Core.Repositories;
using MMS.Service.Products.Queries;
using MMS.Service.Products.Responses;

namespace MMS.Service.Products
{
    public class ProductQueryHandler : IRequestHandler<GetProductListQuery, PagedList<ProductListResponse>>
    {
        private readonly IRepository<Product> _repository;

        public ProductQueryHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<ProductListResponse>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetMappedPagedList<ProductListResponse, GetProductListQuery>(
                ProductListResponse.Configuration, request, cancellationToken: cancellationToken);
        }
    }
}
