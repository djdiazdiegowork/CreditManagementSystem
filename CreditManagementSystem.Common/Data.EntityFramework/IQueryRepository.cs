using System;
using System.Linq;
using System.Linq.Expressions;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public interface IQueryRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Get all entities.
        /// </summary>
        IQueryable<TEntity> FindAll(params string[] include);

        /// <summary>
        /// Get entities with conditions.
        /// </summary>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params string[] include);
    }
}
