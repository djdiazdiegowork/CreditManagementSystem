﻿using Microsoft.EntityFrameworkCore;
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
            var propertiesName = typeof(TEntity).GetProperties().Where(p => p.Name != nameof(IEntity.ID)).Select(p => p.Name);

            var dbEntities = await queryRepository.FindAll().ToArrayAsync();

            foreach (var (dbEntity, entity) in from dbEntity in dbEntities
                                               let entity = entities.FirstOrDefault(entity => entity.ID.Equals(dbEntity.ID))
                                               select (dbEntity, entity))
            {
                if (entity != null)
                {
                    foreach (var (propertyName, value) in from propertyName in propertiesName
                                                          let value = entity.GetType().GetProperty(propertyName).GetValue(entity)
                                                          select (propertyName, value))
                    {
                        dbEntity.GetType().GetProperty(propertyName).SetValue(dbEntity, value);
                    }

                    repository.Update(dbEntity);
                }
                else
                {
                    repository.Delete(dbEntity);
                }
            }

            foreach (var entity in entities.Where(entity => !dbEntities.Any(dbEntity => dbEntity.ID.Equals(entity.ID))))
            {
                repository.Add(entity);
            }

            await unitOfWork.SaveChangesAsync(new System.Threading.CancellationToken());
        }
    }

}
