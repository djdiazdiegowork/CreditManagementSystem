using CreditManagementSystem.Common.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CreditManagementSystem.Data.EntityFramework
{
    public class CreditManagementSystemDbContext : EFDbContext
    {
        public CreditManagementSystemDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public class CreditManagementSystemReadOnlyDbContext : CreditManagementSystemDbContext
        {
            public CreditManagementSystemReadOnlyDbContext(DbContextOptions<CreditManagementSystemReadOnlyDbContext> options)
                : base(options)
            {
            }
        }

        public class CreditManagementSystemReadWriteDbContext : CreditManagementSystemDbContext
        {
            public CreditManagementSystemReadWriteDbContext(DbContextOptions<CreditManagementSystemReadWriteDbContext> options)
                : base(options)
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

                return new CreditManagementSystemReadWriteDbContext(options);
            }
        }
    }
}