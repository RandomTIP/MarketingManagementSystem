using AutoMapper;
using MMS.Service.Distributors;
using MMS.Service.Products;
using MMS.Service.Sales;

namespace MMS.Service.Common.POCO
{
    public class PreviousCodeModel
    {
        public string? Code { get; set; }

        public static IConfigurationProvider DistributorConfiguration => new MapperConfiguration(cfg =>
            cfg.CreateProjection<Distributor, PreviousCodeModel>());

        public static IConfigurationProvider ProductConfiguration =>
            new MapperConfiguration(cfg => cfg.CreateProjection<Product, PreviousCodeModel>());

        public static IConfigurationProvider SaleConfiguration =>
            new MapperConfiguration(cfg => cfg.CreateProjection<Sale, PreviousCodeModel>());
    }
}
