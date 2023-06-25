using MMS.Service.Common.Enums;
using MMS.Service.Distributors.ContactInfo.ContactTypes;

namespace MMS.Service.Distributors.ContactInfo
{
    public partial class ContactInformation
    {
        public int ContactTypeId { get; private set; }
        public ContactInformationType? ContactInformationType { get; private set; }
        public ContactType ContactType => (ContactType)ContactTypeId;
        public string Contact { get; private set; }
    }
}
