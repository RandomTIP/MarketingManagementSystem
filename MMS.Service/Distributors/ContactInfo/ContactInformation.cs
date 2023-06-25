using MMS.Core.Domain;
using MMS.Service.Distributors.ContactInfo.Commands;

namespace MMS.Service.Distributors.ContactInfo
{
    public partial class ContactInformation : AggregateRoot
    {
        private ContactInformation() { }

        public ContactInformation(CreateContactInformationCommand command)
        {
            ContactTypeId = command.ContactTypeId;
            Contact = command.Contact;
        }

        public void Update(UpdateContactInformationCommand command)
        {
            ContactTypeId = command.ContactTypeId;
            Contact = command.Contact;

            LastModifiedDate = DateTime.Now;
        }
    }
}
