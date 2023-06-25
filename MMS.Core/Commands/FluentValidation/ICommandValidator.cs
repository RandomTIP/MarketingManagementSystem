namespace MMS.Core.Commands.FluentValidation;

internal interface ICommandValidator<in TCommand> where TCommand : BaseCommand<TCommand>
{
    void ValidateCommand(TCommand command);
}