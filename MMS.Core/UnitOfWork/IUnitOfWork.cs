namespace MMS.Core.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<IUnitOfWorkScope> CreteScopeAsync(CancellationToken cancellationToken = default);
}