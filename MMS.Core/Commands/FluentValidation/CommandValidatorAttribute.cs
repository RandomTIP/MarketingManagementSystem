using MMS.Core.Extensions;

namespace MMS.Core.Commands.FluentValidation
{
    public class CommandValidatorAttribute : Attribute
    {
        public Type ValidatorType { get; }

        public CommandValidatorAttribute(Type validatorType)
        {
            if (validatorType == null)
            {
                throw new ArgumentNullException(nameof(validatorType));
            }

            if (!validatorType.IsAssignableToGenericType(typeof(ICommandValidator<>)))
            {
                throw new ArgumentException("Validator is not valid!");
            }

            ValidatorType = validatorType;
        }
    }
}
