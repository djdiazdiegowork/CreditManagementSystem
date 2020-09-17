using CreditManagementSystem.Common.Extension;
using Microsoft.EntityFrameworkCore;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public class EFDbContext : DbContext, IUnitOfWork
    {

        public EFDbContext(DbContextOptions options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntities();

            base.OnModelCreating(modelBuilder);
        }

        //public sealed class EFReadDbContext : EFDbContext
        //{
        //    public EFReadDbContext([NotNull] DbContextOptions<EFReadDbContext> options) : base(options)
        //    {
        //        this.ChangeTracker.AutoDetectChangesEnabled = false;
        //        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //    }
        //}

        //public sealed class EFWriteDbContext : EFDbContext
        //{
        //    public EFWriteDbContext([NotNull] DbContextOptions<EFWriteDbContext> options) : base(options)
        //    {

        //    }

        //    public EFDbContext.EFWriteDbContext CreateDbContext(string[] args)
        //    {
        //        var options = new DbContextOptionsBuilder<EFDbContext.EFWriteDbContext>().UseMySQL()
        //            .Options;

        //        return new EFDbContext.EFWriteDbContext(options);
        //    }
        //}    
    }
}
