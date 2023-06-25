using MMS.Service.Common.Enums;
using MMS.Service.Distributors.AddressInfo.AddressTypes;

namespace MMS.Service.Distributors.AddressInfo
{
    public partial class AddressInformation
    {
        public int AddressTypeId { get; private set; }
        public AddressInformationType? AddressInformationType { get; set; }
        public AddressType AddressType => (AddressType)AddressTypeId;
        public string Address { get; private set; }
    }
}
