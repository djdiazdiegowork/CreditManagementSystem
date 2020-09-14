using CreditManagementSystem.Common.Extension;
using Microsoft.EntityFrameworkCore;

namespace CreditManagementSystem.Data.EntityFramework
{
    public class CreditManagementSystemDbContext : DbContext
    {

        public CreditManagementSystemDbContext(DbContextOptions<CreditManagementSystemDbContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            //this.ChangeTracker.AutoDetectChangesEnabled = false;
            //this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntities();
            //modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);

            base.OnModelCreating(modelBuilder);
        }
    }
}