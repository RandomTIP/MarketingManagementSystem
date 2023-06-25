namespace MMS.Service.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(int id, Type entityType) : base($"Entity of type {entityType.Name} with id - {id} was not found!")
        {
            
        }

        public EntityNotFoundException(string message) : base(message)
        {
            
        }
    }
}
