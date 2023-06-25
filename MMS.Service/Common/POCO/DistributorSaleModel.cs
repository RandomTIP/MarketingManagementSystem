namespace MMS.Service.Common.POCO
{
    public class DistributorSaleModel
    {
        public int DistributorId { get; set; }
        public decimal TotalSales { get; set; }
        public decimal FirstLevelSales { get; set; }
        public decimal SecondLevelSales { get; set; }

    }
}
