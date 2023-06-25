#nullable disable

using FluentValidation;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Service.Distributors.ContactInfo.Commands
{
    [CommandValidator(typeof(CreateContactInformationCommandValidator))]
    public class CreateContactInformationCommand : BaseCommand<CreateContactInformationCommand>
    {
        public int ContactTypeId { get; set; }
        public string Contact { get; set; }
    }

    public class CreateContactInformationCommandValidator : CommandValidator<CreateContactInformationCommand>
    {
        public CreateContactInformationCommandValidator()
        {
            RuleFor(x => x.ContactTypeId).NotNull().GreaterThan(0);
            RuleFor(x => x.Contact).NotNull().NotEmpty().Length(1, 100);
        }
    }
}
