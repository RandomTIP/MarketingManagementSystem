using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MMS.Core.Repositories;

namespace MMS.Core.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private static readonly object Lock = new();

        private readonly IServiceProvider _serviceProvider;

        private bool _disposed;
        private UnitOfWorkScope? _currentScope;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IUnitOfWorkScope> CreteScopeAsync(CancellationToken cancellationToken = default)
        {
            lock (Lock)
            {
                _currentScope ??= new UnitOfWorkScope(GetCurrentDbContext());
            }

            await _currentScope.BeginAsync(cancellationToken);

            return _currentScope;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _currentScope?.Dispose();
            }

            _disposed = true;
        }

        private DbContext GetCurrentDbContext()
        {
            var repositoryOptions = _serviceProvider.GetService<RepositoryOptions>();

            if (repositoryOptions == null)
            {
                throw new ArgumentException("RepositoryOptions was not initialized in DI Container!");
            }

            var contextObj = _serviceProvider.GetService(repositoryOptions.DbContextType);

            if (contextObj is not DbContext dbContext)
            {
                throw new ArgumentException("DbContext was not found in the DI Container!");
            }

            return dbContext;
        }
    }
}
