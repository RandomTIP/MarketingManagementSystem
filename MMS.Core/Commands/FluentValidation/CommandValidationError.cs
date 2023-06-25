namespace MMS.Core.Commands.FluentValidation
{
    public class CommandValidationError
    {
        public string PropertyName { get; }
        public string ErrorCode { get; }
        public string ErrorMessage { get; }

        public CommandValidationError(string propertyName, string errorCode, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            var res = $$"""
                ErrorMessage: {{ErrorMessage}}
                ErrorCode: {{ErrorCode}}
                PropertyName: {{PropertyName}}
                """;

            return res;
        }
    }
}
