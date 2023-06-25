using MMS.Core.Domain;
using MMS.Core.Generators;
using MMS.Service.Sales.Command;

namespace MMS.Service.Sales
{
    public partial class Sale : AggregateRoot
    {
        private Sale() { }

        public Sale(CreateSaleCommand command, string? previousCode)
        {
            Code = StringGenerator.GenerateConsequentNumbers(8, previousCode, "SL");
            DistributorId = command.DistributorId;
            SaleDate = command.SaleDate;
            ProductId = command.ProductId;
            ProductCount = command.ProductCount;
            TotalPrice = command.ProductCount * command.ProductUnitPrice;
            IsCalculated = false;
        }

        public void IncludeInCalculation()
        {
            IsCalculated = true;
            CalculationDate = DateTime.Now;
        }
    }
}
