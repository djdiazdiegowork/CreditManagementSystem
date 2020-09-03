using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditManagementSystem.Data.EntityFramework.Configuration
{
    public abstract class CreditManagementSystemEntityTypeBuilder<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name);
        }
    }
}
