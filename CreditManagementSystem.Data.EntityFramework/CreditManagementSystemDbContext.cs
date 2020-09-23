using CreditManagementSystem.Common.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace CreditManagementSystem.Data.EntityFramework
{
    public class CreditManagementSystemDbContext : EFDbContext
    {
        public CreditManagementSystemDbContext(IServiceProvider provider, DbContextOptions options)
            : base(provider, options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public class CreditManagementSystemReadOnlyDbContext : CreditManagementSystemDbContext
        {
            public CreditManagementSystemReadOnlyDbContext(
                IServiceProvider provider,
                DbContextOptions<CreditManagementSystemReadOnlyDbContext> options)
                : base(provider, options)
            {
            }
        }

        public class CreditManagementSystemReadWriteDbContext : CreditManagementSystemDbContext
        {
            public CreditManagementSystemReadWriteDbContext(
                 IServiceProvider provider,
                 DbContextOptions<CreditManagementSystemReadWriteDbContext> options)
                 : base(provider, options)
            {
            }
        }

        public class CreditManagementSystemDbContextFactory : IDesignTimeDbContextFactory<CreditManagementSystemReadWriteDbContext>
        {
            public CreditManagementSystemReadWriteDbContext CreateDbContext(string[] args)
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var builder = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.{environment}.json"))
                    .Build();
                var connectionString = builder.GetConnectionString("CMS_Api_Main");

                var options = new DbContextOptionsBuilder<CreditManagementSystemReadWriteDbContext>()
                    .UseMySQL(connectionString)
                    .Options;

                return new CreditManagementSystemReadWriteDbContext(null, options);
            }
        }
    }
}