using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MMS.Core.Extensions;
using MMS.Data.Context;
using MMS.Service.Common;

namespace MMS.API
{
    public static class ServiceCollectionSetup
    {
        public static void Setup(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddSwagger(environment);
            services.InitializeCore(configuration);
            services.AddMediatR(typeof(ServiceAssembly));
        }

        public static void MigrateDatabase(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MmsDbContext>();

            dbContext?.Database.Migrate();
        }

        private static void InitializeCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextAndRepositories<MmsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MMS")));
            services.AddUnitOfWork();
            services.AddFilters();

            var saveFileDirectory = configuration.GetSection("SaveFileDirectory").Value;
            if (saveFileDirectory != null)
            {
                services.AddFileManager(saveFileDirectory);
            }
        }

        private static void AddSwagger(this IServiceCollection serviceCollection, IHostEnvironment hostEnvironment)
        {
            serviceCollection.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MMS.API",
                    Version = "v1"
                });

                var xmlDocFile = Path.Combine(AppContext.BaseDirectory, $"{hostEnvironment.ApplicationName}.xml");
                if (File.Exists(xmlDocFile))
                {
                    swaggerOptions.IncludeXmlComments(xmlDocFile);
                }
            });
        }
    }
}
