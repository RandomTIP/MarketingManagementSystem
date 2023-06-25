using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MMS.Core.Domain
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; protected set; }

        public bool IsTransient()
        {
            return Id <= 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
            {
                return false;
            }

            if (IsTransient() && other.IsTransient())
            {
                return ReferenceEquals(this, obj);
            }

            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"[{GetType().Name}{Id}]";
        }
    }
}
