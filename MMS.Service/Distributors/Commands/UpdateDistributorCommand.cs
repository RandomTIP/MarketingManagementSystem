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
    [CommandValidator(typeof(UpdateDistributorCommandValidator))]
    public class UpdateDistributorCommand : BaseCommand<UpdateDistributorCommand>, IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderTypeId { get; set; }
        public IFormFile? Picture { get; set; }
        public int? RecommendationAuthorDistributorId { get; set; }
        public UpdateIdentityDocumentCommand IdentityDocumentCommand { get; set; }
        public UpdateContactInformationCommand ContactInformationCommand { get; set; }
        public UpdateAddressInformationCommand AddressInformationCommand { get; set; }

        public override void Validate()
        {
            base.Validate();

            IdentityDocumentCommand.Validate();
            ContactInformationCommand.Validate();
            AddressInformationCommand.Validate();
        }
    }

    public class UpdateDistributorCommandValidator : CommandValidator<UpdateDistributorCommand>
    {
        public UpdateDistributorCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.FirstName).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.LastName).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.BirthDate).NotNull();
            RuleFor(x => x.GenderTypeId).NotNull().GreaterThan(0);
            RuleFor(x => x.RecommendationAuthorDistributorId).GreaterThan(0)
                .When(x => x.RecommendationAuthorDistributorId.HasValue);
        }
    }
}
