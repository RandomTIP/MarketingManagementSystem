using MMS.Service.Distributors;
using MMS.Service.Products;

namespace MMS.Service.Sales
{
    public partial class Sale
    {
        public string Code { get; private set; }
        public int DistributorId { get; private set; }
        public Distributor? Distributor { get; private set; }
        public DateTime SaleDate { get; private set; }
        public int ProductId { get; private set; }
        public Product? Product { get; private set; }
        public int ProductCount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public bool IsCalculated { get; private set; }
        public DateTime CalculationDate { get; private set; }
    }
}
