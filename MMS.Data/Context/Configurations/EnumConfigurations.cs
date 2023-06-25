using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMS.Service.Common.Enums;
using MMS.Service.Distributors.AddressInfo.AddressTypes;
using MMS.Service.Distributors.ContactInfo.ContactTypes;
using MMS.Service.Distributors.GenderTypes;
using MMS.Service.Distributors.IdentityDocuments.DocumentTypes;

namespace MMS.Data.Context.Configurations
{
    public class AddressTypeConfiguration : IEntityTypeConfiguration<AddressInformationType>
    {
        public void Configure(EntityTypeBuilder<AddressInformationType> builder)
        {
            builder.HasData(
                new { Id = (int)AddressType.Factual, Name = AddressType.Factual.ToString() },
                new { Id = (int)AddressType.Registered, Name = AddressType.Registered.ToString() });
        }
    }

    public class ContactTypeConfiguration : IEntityTypeConfiguration<ContactInformationType>
    {
        public void Configure(EntityTypeBuilder<ContactInformationType> builder)
        {
            builder.HasData(
                new { Id = (int)ContactType.Telephone, Name = ContactType.Telephone.ToString() },
                new { Id = (int)ContactType.Mobile, Name = ContactType.Mobile.ToString() },
                new { Id = (int)ContactType.Email, Name = ContactType.Email.ToString() },
                new { Id = (int)ContactType.Fax, Name = ContactType.Fax.ToString() });

        }
    }

    public class GenderTypeConfiguration : IEntityTypeConfiguration<GenderTypeModel>
    {
        public void Configure(EntityTypeBuilder<GenderTypeModel> builder)
        {
            builder.HasData(
                new { Id = (int)GenderType.Male, Name = GenderType.Male.ToString() },
                new { Id = (int)GenderType.Female, Name = GenderType.Female.ToString() });
        }
    }

    public class IdentityDocumentTypeConfiguration : IEntityTypeConfiguration<IdentityDocumentTypeModel>
    {
        public void Configure(EntityTypeBuilder<IdentityDocumentTypeModel> builder)
        {
            builder.HasData(
                new { Id = (int)IdentityDocumentType.IdCard, Name = IdentityDocumentType.IdCard.ToString() },
                new { Id = (int)IdentityDocumentType.Passport, Name = IdentityDocumentType.Passport.ToString() });
        }
    }
}
