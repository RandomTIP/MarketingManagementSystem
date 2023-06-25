using System.Collections.Concurrent;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Core.Commands
{
    public abstract class BaseCommand<TCommand> where TCommand : BaseCommand<TCommand>
    {
        private static readonly ConcurrentDictionary<Type, List<ICommandValidator<TCommand>>> Validators = new();

        public Guid? CommandId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? TimeStamp { get; set; }

        public virtual void Validate()
        {
            GetValidators()?.ForEach(x => x.ValidateCommand((TCommand)this));
        }

        private List<ICommandValidator<TCommand>> GetValidators()
        {
            var commandType = this.GetType();

            if (Validators.TryGetValue(commandType, out var validators))
            {
                return validators;
            }

            var validatorTypes = commandType
                .GetCustomAttributes(true)
                .OfType<CommandValidatorAttribute>()
                .ToArray();

            validators = new List<ICommandValidator<TCommand>>();

            foreach (var validatorType in validatorTypes)
            {
                if (Activator.CreateInstance(validatorType.ValidatorType) is ICommandValidator<TCommand> validator)
                {
                    validators.Add(validator);
                }
            }

            Validators.AddOrUpdate(commandType, validators, (_, _) => validators);

            return validators;
        }
    }
}
