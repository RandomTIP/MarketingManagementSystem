#nullable disable

using FluentValidation;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Service.Distributors.ContactInfo.Commands
{
    [CommandValidator(typeof(UpdateContactInformationCommandValidator))]
    public class UpdateContactInformationCommand : BaseCommand<UpdateContactInformationCommand>
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public string Contact { get; set; }
    }

    public class UpdateContactInformationCommandValidator : CommandValidator<UpdateContactInformationCommand>
    {
        public UpdateContactInformationCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.ContactTypeId).NotNull().GreaterThan(0);
            RuleFor(x => x.Contact).NotNull().NotEmpty().Length(1, 100);
        }
    }
}
