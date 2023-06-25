using AutoMapper;

namespace MMS.Service.Products.Responses
{
    public class ProductListResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        public static IConfigurationProvider Configuration =>
            new MapperConfiguration(cfg => cfg.CreateProjection<Product, ProductListResponse>());
    }
}
