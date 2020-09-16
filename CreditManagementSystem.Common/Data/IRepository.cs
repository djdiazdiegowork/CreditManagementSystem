using System.Threading;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        EntityState Add(TEntity entity);

        void AddRange(params TEntity[] entities);

        Task<EntityState> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(CancellationToken cancellationToken = default, params TEntity[] entities);

        EntityState Update(TEntity entity);

        void UpdateRange(params TEntity[] entities);

        Task<EntityState> DeleteByIDAsync(object id);

        EntityState Delete(TEntity entity);

        void RemoveRange(params TEntity[] entities);

    }
}
