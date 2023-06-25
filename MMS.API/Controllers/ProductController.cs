using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMS.Core.Pagination;
using MMS.Core.Queries;
using MMS.Service.Products.Commands;
using MMS.Service.Products.Queries;
using MMS.Service.Products.Responses;

namespace MMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ValidateModel]
        public async Task<ActionResult<PagedList<ProductListResponse>>> GetPagedListAsync(
            [FromQuery] GetProductListQuery request, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync([FromBody] CreateProductCommand command,
            CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
