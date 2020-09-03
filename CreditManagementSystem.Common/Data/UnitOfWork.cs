using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable
        where TContext : DbContext
    {
        private readonly TContext _context;
        private bool disposed = false;

        public UnitOfWork(TContext context)
        {
            this._context = context;
        }

        public int SaveChanges()
        {
            return this.SaveChangesAsync(new CancellationToken()).Result;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this._context.SaveChangesAsync(cancellationToken);
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this.disposed = true;
        }

        #endregion
    }
}
