using MMS.Service.Common.Enums;
using MMS.Service.Distributors.AddressInfo;
using MMS.Service.Distributors.ContactInfo;
using MMS.Service.Distributors.GenderTypes;
using MMS.Service.Distributors.IdentityDocuments;

namespace MMS.Service.Distributors
{
    public partial class Distributor
    {
        public string Code { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public DateTime BirthDate { get; private set; }
        public int GenderTypeId { get; private set; }
        public GenderTypeModel? GenderType { get; private set; }
        public GenderType Gender => (GenderType)GenderTypeId;
        public string? PictureFileName { get; private set; }
        public int IdentityDocumentId { get; private set; }
        public IdentityDocument? IdentityDocument { get; private set; }
        public int ContactInformationId { get; private set; }
        public ContactInformation? ContactInformation { get; private set; }
        public int AddressInformationId { get; private set; }
        public AddressInformation? AddressInformation { get; private set; }
        public int RecommendationsCount { get; private set; }
        public int? RecommendationAuthorDistributorId { get; private set; }
        public Distributor? RecommendationAuthorDistributor { get; private set; }
        public int RecommendationHierarchyLevel { get; private set; }
        public List<Distributor>? RecommendedDistributors { get; private set; }
    }
}
