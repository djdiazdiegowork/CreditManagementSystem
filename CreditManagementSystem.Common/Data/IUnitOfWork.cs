using System.Threading;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save changes.
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
