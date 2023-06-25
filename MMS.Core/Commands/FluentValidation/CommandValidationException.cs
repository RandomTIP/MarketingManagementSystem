namespace MMS.Core.Commands.FluentValidation
{
    public class CommandValidationException : Exception
    {
        public CommandValidationException() : base("Command Validation Error!")
        {

        }

        public CommandValidationException(IEnumerable<CommandValidationError?> errors) : base(string.Join(";",
            errors.Select(x => x?.ToString())))
        {

        }

        public CommandValidationException(string message, IEnumerable<CommandValidationError?> errors, Exception innerException) : base(
            $"{message}; {string.Join(";", errors.Select(x => x?.ToString()))}", innerException)
        {

        }

        public CommandValidationException(IEnumerable<CommandValidationError?> errors, Exception innerException) : base(
            string.Join(";", errors.Select(x => x?.ToString())), innerException)
        {

        }
    }
}
