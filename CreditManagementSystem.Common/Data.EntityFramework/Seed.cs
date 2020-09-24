using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public abstract class Seed<TEntity> : ISeed<TEntity> where TEntity : class, IEnumeration
    {
        public virtual async Task SeedAsync(DbContext dbContext)
        {
            var entities = typeof(TEntity).GetFields(BindingFlags.Public | BindingFlags.Static).Select(f => (TEntity)f.GetValue(f)).ToArray();
            var propertiesName = typeof(TEntity).GetProperties().Where(p => p.Name != nameof(IEntity.ID)).Select(p => p.Name).ToArray();

            var dbEntities = await dbContext.Set<TEntity>().ToArrayAsync();

            var pairs = (from dbEntity in dbEntities
                         let entity = entities.FirstOrDefault(entity => entity.ID.Equals(dbEntity.ID))
                         select (dbEntity, entity)).ToArray();

            foreach (var (dbEntity, entity) in pairs)
            {
                if (entity != null)
                {
                    var pairsProperties = (from propertyName in propertiesName
                                           let value = entity.GetType().GetProperty(propertyName).GetValue(entity)
                                           select (propertyName, value)).ToArray();

                    foreach (var (propertyName, value) in pairsProperties)
                    {
                        dbEntity.GetType().GetProperty(propertyName).SetValue(dbEntity, value);
                    }

                    dbContext.Update(dbEntity);
                }
                else
                {
                    dbContext.Remove(dbEntity);
                }
            }

            foreach (var entity in entities.Where(entity => !dbEntities.Any(dbEntity => dbEntity.ID.Equals(entity.ID))))
            {
                dbContext.Add(entity);
            }

            await dbContext.SaveChangesAsync();
        }
    }

}
