using CreditManagementSystem.Common.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Services
{
    public interface ICreditStatusService : IService
    {
        /// <summary>
        /// Get all CreditStatus.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        Task<IEnumerable<TEntity>> GetAll<TEntity>();
    }
}
