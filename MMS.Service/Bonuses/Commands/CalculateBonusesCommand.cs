using FluentValidation;
using MediatR;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Service.Bonuses.Commands
{
    [CommandValidator(typeof(CalculateBonusesCommandValidator))]
    public class CalculateBonusesCommand : BaseCommand<CalculateBonusesCommand>, IRequest<int>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CalculateBonusesCommandValidator : CommandValidator<CalculateBonusesCommand>
    {
        public CalculateBonusesCommandValidator()
        {
            RuleFor(x => x.StartDate).NotNull().LessThanOrEqualTo(x => x.EndDate);
            RuleFor(x => x.EndDate).NotNull().GreaterThanOrEqualTo(x => x.StartDate);
        }
    }
}
