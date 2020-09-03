using CreditManagementSystem.Common.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Services
{
    public interface IRiskCenterService : IService
    {
        /// <summary>
        /// Get all Risk.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        Task<IEnumerable<TEntity>> GetAll<TEntity>();
    }
}
