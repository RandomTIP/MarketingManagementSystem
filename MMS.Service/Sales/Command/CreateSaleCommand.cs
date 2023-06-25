using FluentValidation;
using MediatR;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Service.Sales.Command
{
    [CommandValidator(typeof(CreateSaleCommandValidator))]
    public class CreateSaleCommand : BaseCommand<CreateSaleCommand>, IRequest<int>
    {
        public int DistributorId { get; set; }
        public DateTime SaleDate { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public decimal ProductUnitPrice { get; set; }
    }

    public class CreateSaleCommandValidator : CommandValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.DistributorId).NotNull().GreaterThan(0);
            RuleFor(x => x.SaleDate).NotNull();
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductCount).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductUnitPrice).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
