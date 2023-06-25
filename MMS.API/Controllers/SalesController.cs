using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMS.Service.Sales.Command;
using MMS.Service.Sales.Queries;
using MMS.Service.Sales.Responses;

namespace MMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalesListResponse>>> GetListAsync([FromQuery] GetSalesListQuery request,
            CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync([FromBody] CreateSaleCommand command,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
