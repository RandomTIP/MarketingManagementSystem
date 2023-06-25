using FluentValidation;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Service.Distributors.AddressInfo.Commands
{
    [CommandValidator(typeof(UpdateAddressInformationCommandValidator))]
    public class UpdateAddressInformationCommand : BaseCommand<UpdateAddressInformationCommand>
    {
        public int Id { get; set; }
        public int AddressTypeId { get; set; }
        public string Address { get; set; }
    }

    public class UpdateAddressInformationCommandValidator : CommandValidator<UpdateAddressInformationCommand>
    {
        public UpdateAddressInformationCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.AddressTypeId).NotNull().GreaterThan(0);
            RuleFor(x => x.Address).NotNull().NotEmpty().Length(1, 100);
        }
    }
}
