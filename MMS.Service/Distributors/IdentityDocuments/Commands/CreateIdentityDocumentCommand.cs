using FluentValidation;
using MMS.Core.Commands;
using MMS.Core.Commands.FluentValidation;

namespace MMS.Service.Distributors.IdentityDocuments.Commands
{
    [CommandValidator(typeof(CreateIdentityDocumentCommandValidator))]
    public class CreateIdentityDocumentCommand : BaseCommand<CreateIdentityDocumentCommand>
    {
        public int IdentityDocumentTypeId { get; set; }
        public string? SerialNumber { get; set; }
        public string? DocNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string PersonalNumber { get; set; } = string.Empty;
        public string? IssuerName { get; set; }
    }

    public class CreateIdentityDocumentCommandValidator : CommandValidator<CreateIdentityDocumentCommand>
    {
        public CreateIdentityDocumentCommandValidator()
        {
            RuleFor(x => x.IdentityDocumentTypeId).NotNull().GreaterThan(0);
            RuleFor(x => x.SerialNumber).Length(1, 10).When(x => !string.IsNullOrEmpty(x.SerialNumber));
            RuleFor(x => x.DocNumber).Length(1, 10).When(x => !string.IsNullOrEmpty(x.DocNumber));
            RuleFor(x => x.IssueDate).NotNull().LessThan(x => x.ExpiryDate);
            RuleFor(x => x.ExpiryDate).NotNull().GreaterThan(x => x.IssueDate);
            RuleFor(x => x.PersonalNumber).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.IssuerName).Length(1, 100).When(x => !string.IsNullOrEmpty(x.IssuerName));
        }
    }
}
