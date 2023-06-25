using MMS.Core.Domain;

namespace MMS.Service.Distributors.ContactInfo.ContactTypes
{
    public class ContactInformationType : CommonEntity
    {
        private ContactInformationType() { }
        public ContactInformationType(string name)
        {
            Name = name;
        }
    }
}
