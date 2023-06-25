using MMS.Service.Common.Enums;
using MMS.Service.Distributors.IdentityDocuments.DocumentTypes;

namespace MMS.Service.Distributors.IdentityDocuments
{
    public partial class IdentityDocument
    {
        public int IdentityDocumentTypeId { get; private set; }
        public IdentityDocumentTypeModel? IdentityDocumentTypeModel { get; private set; }
        public IdentityDocumentType IdentityDocumentType => (IdentityDocumentType)IdentityDocumentTypeId;
        public string? SerialNumber { get; private set; }
        public string? DocumentNumber { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public string PersonalNumber { get; private set; }
        public string? IssuerName { get; private set; }
    }
}
