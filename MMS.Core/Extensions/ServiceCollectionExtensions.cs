using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MMS.Core.FileManagers;
using MMS.Core.Filtering;
using MMS.Core.Repositories;
using MMS.Core.UnitOfWork;

namespace MMS.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFileManager(this IServiceCollection services, string saveFileDirectory)
        {
            if(services.Any(x => x.ServiceType == typeof(FileManager)))
            {
                return;
            }

            var fileManager = new FileManager(saveFileDirectory);
            services.AddSingleton<IFileManager>(fileManager);
        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }

        public static void AddFilters(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x =>
            {
                var assemblyName = x.GetName().FullName;
                return !assemblyName.Contains("Microsoft")
                       && !assemblyName.Contains("System")
                       && !assemblyName.Contains("netcore")
                       && !assemblyName.Contains("Swashbuckle");
            });

            foreach (var assembly in assemblies)
            {
                var filterServiceImplementationTypes = assembly.GetTypes().Where(x =>
                    x.IsClass && x.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IFilterService<,>)))
                    .ToList();

                if(filterServiceImplementationTypes.Count == 0)
                {
                    continue;
                }

                foreach (var implementationType in filterServiceImplementationTypes)
                {
                    AddFilterService(services, implementationType);
                }
            }
        }

        public static void AddFilterService<TImplementation>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var implementingType = typeof(TImplementation);
            
            AddFilterService(services, implementingType, lifetime);
        }

        public static void AddFilterService(this IServiceCollection services, Type implementingType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var entityAndFilterTypes = GetFilterGenericArgumentTypes(implementingType);
            var filterServiceType = typeof(IFilterService<,>).MakeGenericType(entityAndFilterTypes.EntityType, entityAndFilterTypes.FilterType);

            if (!implementingType.IsAssignableTo(filterServiceType))
            {
                throw new ArgumentException(
                    $"{implementingType.Name} does not implement {typeof(IFilterService<,>)}!");
            }

            if (services.Any(x => x.ServiceType == filterServiceType))
            {
                return;
            }

            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    services.AddScoped(filterServiceType, implementingType);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(filterServiceType, implementingType);
                    break;
                case ServiceLifetime.Singleton:
                    services.AddSingleton(filterServiceType, implementingType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public static void AddDbContextAndRepositories<TDbContext>(this IServiceCollection services,
            Action<DbContextOptionsBuilder>? options = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifeTime = ServiceLifetime.Scoped,
            Action<RepositoryOptions>? repositoryOptions = null) where TDbContext : DbContext
        {
            var contextType = typeof(TDbContext);
            services.AddDbContext<TDbContext>(options, contextLifetime, optionsLifeTime);

            var repoOptions = new RepositoryOptions(typeof(TDbContext));
            repositoryOptions?.Invoke(repoOptions);
            services.AddSingleton(repoOptions);

            services.AddRepositories(contextType);
        }

        private static void AddRepositories(this IServiceCollection services, IReflect dbContextType)
        {
            var repositoryInterfaceType = typeof(IRepository<>);
            var repositoryImplementationType = typeof(Repository<>);
            var entityTypes = GetEntityTypes(dbContextType);

            foreach (var entityType in entityTypes)
            {
                var genericRepoInterfaceType = repositoryInterfaceType.MakeGenericType(entityType);
                if(services.Any(x => x.ServiceType == genericRepoInterfaceType))
                {
                    return;
                }

                var genericRepoImplementationType = repositoryImplementationType.MakeGenericType(entityType);
                services.AddScoped(genericRepoInterfaceType, genericRepoImplementationType);
            }
        }

        private static IEnumerable<Type> GetEntityTypes(IReflect dbContextType)
        {
            return dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.IsAssignableToGenericType(typeof(DbSet<>)))
                .Select(x => x.PropertyType.GenericTypeArguments[0]);
        }

        private static (Type EntityType, Type FilterType) GetFilterGenericArgumentTypes(Type filterImplementationType)
        {
            var filterServiceInterfaceType = filterImplementationType.GetInterfaces().FirstOrDefault(x =>
                x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IFilterService<,>));

            if (filterServiceInterfaceType == null)
            {
                throw new ArgumentException(
                    $"{filterImplementationType.Name} does not implement {typeof(IFilterService<,>)}!");
            }

            var entityType = filterServiceInterfaceType.GenericTypeArguments[0];
            var filterType = filterServiceInterfaceType.GenericTypeArguments[1];

            return (entityType, filterType);
        }
    }
}
