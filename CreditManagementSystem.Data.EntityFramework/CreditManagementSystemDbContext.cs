using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Extension;
using CreditManagementSystem.Data.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CreditManagementSystem.Data.EntityFramework
{
    public class CreditManagementSystemDbContext : DbContext
    {

        public CreditManagementSystemDbContext(DbContextOptions<CreditManagementSystemDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            var baseType = typeof(IEntity);
            var types = baseType.GetEntityTypes();

            foreach (var type in types)
            {
                modelbuilder.Entity(type);
            }

            baseType = typeof(CreditManagementSystemEntityTypeBuilder<>);

            modelbuilder.ApplyConfigurationsFromAssembly(baseType.Assembly, t => t.IsClass && !t.IsAbstract );

            base.OnModelCreating(modelbuilder);
        }
    }
}