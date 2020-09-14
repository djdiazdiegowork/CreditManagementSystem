using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _context;

        public Repository(TContext context)
        {
            this._context = context;
        }

        public void Add(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
        }

        public void Add(params TEntity[] entities)
        {
            this._context.Set<TEntity>().AddRange(entities);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            this._context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            this._context.Set<TEntity>().Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(params TEntity[] entities)
        {
            Update(entities.ToList());
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            this._context.Set<TEntity>().AttachRange(entities);

            foreach (var entity in entities)
            {
                this._context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task Delete(object id)
        {
            var entity = await this._context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return;
            }

            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            if (this._context.Entry(entity).State == EntityState.Detached)
            {
                this._context.Set<TEntity>().Attach(entity);
            }

            this._context.Set<TEntity>().Remove(entity);
        }

        public void Delete(params TEntity[] entities)
        {
            Delete(entities.ToList());
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (this._context.Entry(entity).State == EntityState.Detached)
                {
                    this._context.Set<TEntity>().Attach(entity);
                }
            }

            this._context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
