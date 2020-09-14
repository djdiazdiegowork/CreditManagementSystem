using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public class QueryRepository<TEntity, TContext> : IQueryRepository<TEntity>
      where TEntity : class, IEntity
      where TContext : DbContext
    {
        private readonly TContext _context;

        public QueryRepository(TContext context)
        {
            this._context = context;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            var query = this._context.Set<TEntity>().Where(predicate);

            if (include != null)
            {
                foreach (var navigationProperty in include)
                {
                    query = query.Include(navigationProperty);
                }
            }

            return query;
        }

        public IQueryable<TEntity> FindAll(params string[] include)
        {
            var query = (IQueryable<TEntity>)this._context.Set<TEntity>();

            if (include != null)
            {
                foreach (var navigationProperty in include)
                {
                    query = query.Include(navigationProperty);
                }
            }

            return query;
        }
    }

}
