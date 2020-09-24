using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public class Repository<TEntity, TContext> : QueryRepository<TEntity, TContext>, IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _context;

        public Repository(TContext context) : base(context)
        {
            this._context = context;
        }

        public EntityState Add(TEntity entity)
        {
            return (EntityState)this._context.Set<TEntity>().Add(entity).State;
        }

        public void AddRange(params TEntity[] entities)
        {
            this._context.Set<TEntity>().AddRange(entities);
        }

        public async Task<EntityState> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return (EntityState)(await this._context.Set<TEntity>().AddAsync(entity, cancellationToken)).State;
        }

        public Task AddRangeAsync(CancellationToken cancellationToken = default, params TEntity[] entities)
        {
            return this._context.Set<TEntity>().AddRangeAsync(entities);
        }

        public EntityState Update(TEntity entity)
        {
            return (EntityState)this._context.Set<TEntity>().Update(entity).State;
        }

        public void UpdateRange(params TEntity[] entities)
        {
            this._context.Set<TEntity>().UpdateRange(entities);
        }

        public async Task<EntityState> DeleteByIDAsync(object id)
        {
            var entity = await this._context.Set<TEntity>().FindAsync(id);

            return entity != null ? Delete(entity) : EntityState.Detached;
        }

        public EntityState Delete(TEntity entity)
        {
            return (EntityState)this._context.Set<TEntity>().Remove(entity).State;
        }

        public void DeleteRange(params TEntity[] entities)
        {
            this._context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
