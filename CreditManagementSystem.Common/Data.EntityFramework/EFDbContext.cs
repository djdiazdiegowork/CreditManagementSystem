using CreditManagementSystem.Common.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public class EFDbContext : DbContext, IUnitOfWork
    {

        public EFDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntities();

            base.OnModelCreating(modelBuilder);
        }
    }
}
