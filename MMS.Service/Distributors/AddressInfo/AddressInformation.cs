using MMS.Core.Domain;
using MMS.Service.Distributors.AddressInfo.Commands;

namespace MMS.Service.Distributors.AddressInfo
{
    public partial class AddressInformation : AggregateRoot
    {
        private AddressInformation(){}
        public AddressInformation(CreateAddressInformationCommand command)
        {
            AddressTypeId = command.AddressTypeId;
            Address = command.Address;
        }

        public void Update(UpdateAddressInformationCommand command)
        {
            AddressTypeId = command.AddressTypeId;
            Address = command.Address;

            LastModifiedDate = DateTime.Now;
        }
    }
}
