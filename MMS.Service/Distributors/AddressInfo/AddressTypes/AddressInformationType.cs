using MMS.Core.Domain;

namespace MMS.Service.Distributors.AddressInfo.AddressTypes
{
    public class AddressInformationType : CommonEntity
    {
        private AddressInformationType() { }
        public AddressInformationType(string name)
        {
            Name = name;
        }
    }
}
