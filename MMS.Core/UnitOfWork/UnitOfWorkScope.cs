using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MMS.Core.UnitOfWork
{
    internal class UnitOfWorkScope : IUnitOfWorkScope
    {
        private int _index;
        private bool _disposed;

        private readonly DbContext _context;
        private readonly List<IDbContextTransaction> _transactions = new();

        public UnitOfWorkScope(DbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            if (_index == 1)
            {
                await _context.SaveChangesAsync(cancellationToken);
                CommitTransactions();
            }
        }

        internal async Task BeginAsync(CancellationToken cancellationToken)
        {
            if (_index == 0)
            {
                await BeginTransactionsAsync(cancellationToken);
            }

            _index++;
        }

        private async Task BeginTransactionsAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            
            if(_transactions.All(x => x.TransactionId != transaction.TransactionId))
            {
                _transactions.Add(transaction);
            }
        }

        private void CommitTransactions()
        {
            if(_transactions.Count == 0)
            {
                return;
            }

            for (var i = 0; i < _transactions.Count; i++)
            {
                _transactions[i].Commit();
                _transactions[i].Dispose();
                _transactions.RemoveAt(i);
            }
        }

        private void RollBackTransactions()
        {
            if (_transactions.Count == 0)
            {
                return;
            }

            for (var i = 0; i < _transactions.Count; i++)
            {
                _transactions[i].Rollback();
                _transactions[i].Dispose();
                _transactions.RemoveAt(i);
            }
        }

        public void Dispose()
        {
            if (_index > 0)
            {
                _index--;
            }

            if (_index != 0)
            {
                return;
            }

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
                _context.ChangeTracker.Entries()
                    .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                    .ToList()
                    .ForEach(x => x.State = EntityState.Detached);

                RollBackTransactions();

                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
