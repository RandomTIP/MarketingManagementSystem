using FluentValidation;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Service.Distributors.AddressInfo.Commands
{
    [CommandValidator(typeof(CreateAddressInformationCommandValidator))]
    public class CreateAddressInformationCommand : BaseCommand<CreateAddressInformationCommand>
    {
        public int AddressTypeId { get; set; }
        public string Address { get; set; }
    }

    public class CreateAddressInformationCommandValidator : CommandValidator<CreateAddressInformationCommand>
    {
        public CreateAddressInformationCommandValidator()
        {
            RuleFor(x => x.AddressTypeId).NotNull().GreaterThan(0);
            RuleFor(x => x.Address).NotNull().NotEmpty().Length(1, 100);
        }
    }
}
