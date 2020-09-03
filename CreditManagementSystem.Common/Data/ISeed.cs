using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data
{
    public interface ISeed
    {
        public string GetNameSeedAsync => nameof(ISeed<IEntity>.SeedAsync);
    }

    public interface ISeed<TEntity> : ISeed where TEntity : class, IEntity
    {
        Task SeedAsync(IQueryRepository<TEntity> queryRepository, IRepository<TEntity> repository, IUnitOfWork unitOfWork);
    }
}
