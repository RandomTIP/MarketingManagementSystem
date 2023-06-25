using AutoMapper;
using MMS.Service.Distributors;

namespace MMS.Service.Common.POCO
{
    public class IdModel
    {
        public int Id { get; set; }

        public static IConfigurationProvider RecommendationAuthorConfig =>
            new MapperConfiguration(cfg =>
                cfg.CreateProjection<Distributor, IdModel>()
                    .ForMember(x => x.Id, o => o.MapFrom(s => s.RecommendationAuthorDistributorId)));

        public static IConfigurationProvider DistributorIdConfig =>
            new MapperConfiguration(x =>
                x.CreateProjection<Distributor, IdModel>());
    }
}
