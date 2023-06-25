using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;
using MMS.Service.Distributors.AddressInfo.Commands;
using MMS.Service.Distributors.ContactInfo.Commands;
using MMS.Service.Distributors.IdentityDocuments.Commands;

namespace MMS.Service.Distributors.Commands
{
    [CommandValidator(typeof(CreateDistributorCommandValidator))]
    public class CreateDistributorCommand : BaseCommand<CreateDistributorCommand>, IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderTypeId { get; set; }
        public IFormFile? Picture { get; set; }
        public int? RecommendationAuthorDistributorId { get; set; }
        public CreateIdentityDocumentCommand IdentityDocumentCommand { get; set; }
        public CreateContactInformationCommand ContactInformationCommand { get; set; }
        public CreateAddressInformationCommand AddressInformationCommand { get; set; }
        
        public override void Validate()
        {
            base.Validate();

            IdentityDocumentCommand.Validate();
            ContactInformationCommand.Validate();
            AddressInformationCommand.Validate();
        }
    }

    public class CreateDistributorCommandValidator : CommandValidator<CreateDistributorCommand>
    {
        public CreateDistributorCommandValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.LastName).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.BirthDate).NotNull();
            RuleFor(x => x.GenderTypeId).NotNull().GreaterThan(0);
            RuleFor(x => x.RecommendationAuthorDistributorId).GreaterThan(0)
                .When(x => x.RecommendationAuthorDistributorId.HasValue);
        }
    }
}
