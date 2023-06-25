namespace MMS.Core.Repositories
{
    public class RepositoryOptions
    {
        public Type DbContextType { get; }
        public SaveChangesStrategy SaveChangesStrategy { get; }

        public RepositoryOptions(Type dbContextType, SaveChangesStrategy saveChangesStrategy = SaveChangesStrategy.PerUnitOfWork)
        {
            DbContextType = dbContextType;
            SaveChangesStrategy = saveChangesStrategy;
        }
    }

    public enum SaveChangesStrategy
    {
        PerOperation,
        PerUnitOfWork
    }
}
