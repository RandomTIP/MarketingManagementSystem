using MMS.Core.Domain;
using MMS.Service.Distributors.IdentityDocuments.Commands;

namespace MMS.Service.Distributors.IdentityDocuments
{
    public partial class IdentityDocument : AggregateRoot
    {
        private IdentityDocument(){}
        public IdentityDocument(CreateIdentityDocumentCommand command)
        {
            IdentityDocumentTypeId = command.IdentityDocumentTypeId;
            SerialNumber = command.SerialNumber;
            DocumentNumber = command.DocNumber;
            IssueDate = command.IssueDate;
            ExpiryDate = command.ExpiryDate;
            PersonalNumber = command.PersonalNumber;
            IssuerName = command.IssuerName;
        }

        public void Update(UpdateIdentityDocumentCommand command)
        {
            IdentityDocumentTypeId = command.IdentityDocumentTypeId;
            SerialNumber = command.SerialNumber;
            DocumentNumber = command.DocNumber;
            IssueDate = command.IssueDate;
            ExpiryDate = command.ExpiryDate;
            PersonalNumber = command.PersonalNumber;
            IssuerName = command.IssuerName;

            LastModifiedDate = DateTime.Now;
        }
    }
}
