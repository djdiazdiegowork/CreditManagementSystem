using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data
{
    public interface ISeed<TEntity> where TEntity : class, IEntity
    {
        Task SeedAsync(IRepository<TEntity> repository, IUnitOfWork unitOfWork);
    }
}
