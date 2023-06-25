using MMS.Core.Domain;
using MMS.Core.Generators;
using MMS.Service.Common.Exceptions;
using MMS.Service.Distributors.AddressInfo;
using MMS.Service.Distributors.Commands;
using MMS.Service.Distributors.ContactInfo;
using MMS.Service.Distributors.IdentityDocuments;

namespace MMS.Service.Distributors
{
    public partial class Distributor : AggregateRoot
    {
        private Distributor(){}
        public Distributor(CreateDistributorCommand command, string? previousCodeValue)
        {
            Code = StringGenerator.GenerateConsequentNumbers(10, previousCodeValue, "DSTR");
            FirstName = command.FirstName;
            LastName = command.LastName;
            BirthDate = command.BirthDate;
            GenderTypeId = command.GenderTypeId;

            IdentityDocument = new IdentityDocument(command.IdentityDocumentCommand);
            ContactInformation = new ContactInformation(command.ContactInformationCommand);
            AddressInformation = new AddressInformation(command.AddressInformationCommand);
        }

        public void Update(UpdateDistributorCommand command)
        {
            FirstName = command.FirstName;
            LastName = command.LastName;
            BirthDate = command.BirthDate;
            GenderTypeId = command.GenderTypeId;

            IdentityDocument?.Update(command.IdentityDocumentCommand);
            ContactInformation?.Update(command.ContactInformationCommand);
            AddressInformation?.Update(command.AddressInformationCommand);

            LastModifiedDate = DateTime.Now;
        }

        public void AddPicture(string pictureFileName) => PictureFileName = pictureFileName;

        public void AddRecommendedDistributor(Distributor distributor)
        {
            if (RecommendationHierarchyLevel == 4)
            {
                return;
            }

            if (RecommendationsCount == 3)
            {
                throw new RecommendationCountException();
            }

            RecommendedDistributors ??= new List<Distributor>();
            RecommendedDistributors.Add(distributor);

            RecommendationsCount++;

            distributor.SetRecommendationAuthor(this);
        }

        public void RemoveRecommendedDistributor()
        {
            RecommendationsCount--;
        }

        private void SetRecommendationAuthor(Distributor recommendationAuthor)
        {
            RecommendationAuthorDistributor = recommendationAuthor;
            RecommendationAuthorDistributorId = recommendationAuthor.Id;
            RecommendationHierarchyLevel = recommendationAuthor.RecommendationHierarchyLevel + 1;
        }
    }
}
