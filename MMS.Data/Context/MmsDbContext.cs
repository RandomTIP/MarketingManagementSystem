using Microsoft.EntityFrameworkCore;
using MMS.Service.Bonuses;
using MMS.Service.Distributors;
using MMS.Service.Distributors.AddressInfo;
using MMS.Service.Distributors.AddressInfo.AddressTypes;
using MMS.Service.Distributors.ContactInfo;
using MMS.Service.Distributors.ContactInfo.ContactTypes;
using MMS.Service.Distributors.GenderTypes;
using MMS.Service.Distributors.IdentityDocuments;
using MMS.Service.Distributors.IdentityDocuments.DocumentTypes;
using MMS.Service.Products;
using MMS.Service.Sales;

namespace MMS.Data.Context
{
    public class MmsDbContext : DbContext
    {
        public DbSet<AddressInformation> AddressInformations { get; set; }
        public DbSet<AddressInformationType> AddressInformationTypes { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<ContactInformationType> ContactInformationTypes { get; set; }
        public DbSet<IdentityDocument> IdentityDocuments { get; set; }
        public DbSet<IdentityDocumentTypeModel> IdentityDocumentTypes { get; set; }
        public DbSet<GenderTypeModel> GenderTypes { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Bonus> Bonuses { get; set; }

        public MmsDbContext(DbContextOptions<MmsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(38, 18);
            configurationBuilder.Properties<decimal?>().HavePrecision(38, 18);
        }
    }
}
