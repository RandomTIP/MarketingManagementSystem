using MMS.Core.Domain;
using MMS.Core.Generators;
using MMS.Service.Products.Commands;

namespace MMS.Service.Products
{
    public partial class Product : AggregateRoot
    {
        private Product() { }

        public Product(CreateProductCommand command, string? previousValue)
        {
            Code = StringGenerator.GenerateConsequentNumbers(10, previousValue, "PRD");
            Name = command.Name;
            UnitPrice = command.UnitPrice;
        }
    }
}
