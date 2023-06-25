using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMS.Service.Distributors.Commands;
using MMS.Service.Distributors.Queries;
using MMS.Service.Distributors.Responses;

namespace MMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistributorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DistributorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DistributorListResponse>>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetDistributorListRequest(), cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync([FromForm] CreateDistributorCommand command,
            CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateDistributorCommand command,
            CancellationToken cancellationToken = default)
        {
            _ = await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteDistributorCommand command, CancellationToken cancellationToken = default)
        {
            _ = await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
