using FluentValidation;
using MediatR;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Service.Products.Commands
{
    [CommandValidator(typeof(CreateProductCommandValidator))]
    public class CreateProductCommand : BaseCommand<CreateProductCommand>, IRequest<int>
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CreateProductCommandValidator : CommandValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 100);
            RuleFor(x => x.UnitPrice).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
