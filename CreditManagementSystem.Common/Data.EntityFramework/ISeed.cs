using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public interface ISeed<TEntity> where TEntity : class, IEntity
    {
        Task SeedAsync(IQueryRepository<TEntity> queryRepository, IRepository<TEntity> repository, IUnitOfWork unitOfWork);
    }
}
