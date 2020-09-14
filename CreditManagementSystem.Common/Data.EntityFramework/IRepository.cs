using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Add entity.
        /// </summary>
        void Add(TEntity entity);

        /// <summary>
        /// Add entities.
        /// </summary>
        void Add(params TEntity[] entities);

        /// <summary>
        /// Add entities.
        /// </summary>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Update entities.
        /// </summary>
        void Update(params TEntity[] entities);

        /// <summary>
        /// Update entities.
        /// </summary>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        Task Delete(object id);

        /// <summary>
        /// Delete entity.
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entities.
        /// </summary>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// Delete entities.
        /// </summary>
        void Delete(IEnumerable<TEntity> entities);
    }
}
