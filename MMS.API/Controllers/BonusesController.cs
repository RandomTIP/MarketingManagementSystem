using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MMS.Core.Repositories;
using MMS.Service.Bonuses;
using MMS.Service.Bonuses.Commands;

namespace MMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BonusesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Bonus> _repository;

        public BonusesController(IMediator mediator, IRepository<Bonus> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Bonus>>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetListAsync(cancellationToken: cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CalculateBonusesAsync([FromBody] CalculateBonusesCommand command,
            CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
