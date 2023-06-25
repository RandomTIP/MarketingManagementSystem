using MMS.Core.Domain;

namespace MMS.Service.Distributors.IdentityDocuments.DocumentTypes
{
    public class IdentityDocumentTypeModel : CommonEntity
    {
        private IdentityDocumentTypeModel() { }

        public IdentityDocumentTypeModel(string name)
        {
            Name = name;
        }
    }
}
