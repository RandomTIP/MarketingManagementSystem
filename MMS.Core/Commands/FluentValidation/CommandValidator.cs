using FluentValidation;

namespace MMS.Core.Commands.FluentValidation
{
    public abstract class CommandValidator<TCommand> : AbstractValidator<TCommand>, ICommandValidator<TCommand> where TCommand : BaseCommand<TCommand>
    {
        public void ValidateCommand(TCommand command)
        {
            var res = base.Validate(command);

            if (res is { IsValid: false })
            {
                throw new CommandValidationException(res.Errors.Select(x =>
                    new CommandValidationError(x.PropertyName, x.ErrorCode, x.ErrorMessage)));
            }
        }
    }
}
