using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MMS.Core.Extensions
{
    internal static class Extensions
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType.GetTypeInfo().IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            if (givenType.GetInterfaces().Any(interfaceType => interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }

            return givenType.GetTypeInfo().BaseType != null && givenType.GetTypeInfo().BaseType!.IsAssignableToGenericType(genericType);
        }

        public static IQueryable<TEntity> ApplyIncludes<TEntity>(this IQueryable<TEntity> source, string[]? relatedProperties)
            where TEntity : class
        {
            if (relatedProperties == null || relatedProperties.Length == 0)
            {
                return source;
            }

            var type = typeof(TEntity);

            var entityProps = type.GetProperties().Select(x => x.Name).Where(relatedProperties.Contains);

            foreach (var entityProp in entityProps)
            {
                source.Include(entityProp);
            }

            return source;
        }
    }
}
