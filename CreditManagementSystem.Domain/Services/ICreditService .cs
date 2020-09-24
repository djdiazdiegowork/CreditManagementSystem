using CreditManagementSystem.Common.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Services
{
    public interface ICreditService : IService
    {
        /// <summary>
        /// Get Credit.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        Task<TEntity> Get<TEntity>(Guid id);

        /// <summary>
        /// Get all Credit.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        Task<IEnumerable<TEntity>> GetAll<TEntity>();
    }
}
