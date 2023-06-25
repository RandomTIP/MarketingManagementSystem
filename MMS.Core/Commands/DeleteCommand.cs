using FluentValidation;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Core.Commands
{
    [CommandValidator(typeof(DeleteCommandValidator))]
    public class DeleteCommand : BaseCommand<DeleteCommand>
    {
        public int Id { get; set; }
    }

    public class DeleteCommandValidator : CommandValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
