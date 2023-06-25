#nullable disable

using AutoMapper;

namespace MMS.Service.Sales.Responses
{
    public class SalesListResponse
    {
        public string Code { get; set; }
        public string Distributor { get; set; }
        public DateTime SaleDate { get; set; }
        public string Product { get; set; }
        public string ProductCode { get; set; }
        public decimal? ProductUnitPrice { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCalculated { get; set; }
        public DateTime CalculationDate { get; set; }

        public static IConfigurationProvider Configuration => new MapperConfiguration(cfg => cfg
            .CreateProjection<Sale, SalesListResponse>()
            .ForMember(x => x.Distributor,
                o => o.MapFrom(s => string.Join(" ", s.Distributor!.FirstName, s.Distributor!.LastName)))
            .ForMember(x => x.Product, o => o.MapFrom(s => s.Product!.Name))
            .ForMember(x => x.ProductCode, o => o.MapFrom(s => s.Product!.Code))
            .ForMember(x => x.ProductUnitPrice, o => o.MapFrom(s => s.Product!.UnitPrice)));
    }
}
