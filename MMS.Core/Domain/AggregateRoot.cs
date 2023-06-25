namespace MMS.Core.Domain
{
    public class AggregateRoot : Entity
    {
        public DateTime CreateDate { get; protected set; }
        public DateTime? LastModifiedDate { get; protected set; }
        public DateTime? DeleteDate { get; protected set; }
        public bool IsActive { get; protected set; }

        protected AggregateRoot()
        {
            CreateDate = DateTime.Now;
            IsActive = true;
        }

        public void Delete()
        {
            IsActive = false;
            DeleteDate = DateTime.Now;
        }

        public void Restore()
        {
            IsActive = true;
            DeleteDate = null;
        }
    }
}
