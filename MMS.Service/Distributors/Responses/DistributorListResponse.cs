using AutoMapper;

namespace MMS.Service.Distributors.Responses
{
    public class DistributorListResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string? PictureFileName { get; set; }
        public string PersonalNumber { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int RecommendationsCount { get; set; }
        public string? RecommendationAuthor { get; set; }
        public int RecommendationHierarchyLevel { get; set; }

        public static IConfigurationProvider ConfigurationProvider =>
            new MapperConfiguration(x =>
                x.CreateProjection<Distributor, DistributorListResponse>()
                    .ForMember(d => d.Gender, o => o.MapFrom(s => s.GenderType!.Name))
                    .ForMember(d => d.PersonalNumber, o => o.MapFrom(s => s.IdentityDocument!.PersonalNumber))
                    .ForMember(d => d.Contact, o => o.MapFrom(s => s.ContactInformation!.Contact))
                    .ForMember(d => d.Address, o => o.MapFrom(s => s.AddressInformation!.Address))
                    .ForMember(d => d.RecommendationAuthor,
                        o => o.MapFrom(s => string.Join(" ", s.RecommendationAuthorDistributor!.FirstName,
                            s.RecommendationAuthorDistributor!.LastName))));
    }
}
