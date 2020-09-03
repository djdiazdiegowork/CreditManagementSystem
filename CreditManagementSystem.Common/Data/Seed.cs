using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data
{
    public abstract class Seed<TEntity> : ISeed<TEntity> where TEntity : class, IEnumeration
    {
        public virtual async Task SeedAsync(IQueryRepository<TEntity> queryRepository, IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            var entities = typeof(TEntity).GetFields(BindingFlags.Public | BindingFlags.Static).Select(f => (TEntity)f.GetValue(f));

            var dbEntities = await queryRepository.FindAll().ToArrayAsync();

            foreach (var dbEntity in dbEntities)
            {
                if (!entities.Any(p => dbEntity.ID.Equals(p.ID) && dbEntity.Name.Equals(p.Name)))
                {
                    repository.Delete(dbEntity);
                }
            }

            foreach (var entity in entities)
            {
                if (!dbEntities.Any(p => entity.ID.Equals(p.ID) && entity.Name.Equals(p.Name)))
                {
                    repository.Add(entity);
                }
            }

            await unitOfWork.SaveChangesAsync(new System.Threading.CancellationToken());
        }
    }

}
