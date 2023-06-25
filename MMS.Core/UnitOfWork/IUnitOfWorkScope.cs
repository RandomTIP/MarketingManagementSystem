namespace MMS.Core.UnitOfWork;

public interface IUnitOfWorkScope : IDisposable
{
    Task CompleteAsync(CancellationToken cancellationToken);
}