using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Extension;
using CreditManagementSystem.Common.SequentialGuidGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CreditManagementSystem.Data.EntityFramework.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static void AddDataEFServices(this IServiceCollection services, string connectionStrings)
        {
            services.AddDbContext<CreditManagementSystemDbContext.CreditManagementSystemReadOnlyDbContext>(options =>
            {
                options.UseMySQL(connectionStrings);
            });

            services.AddDbContext<CreditManagementSystemDbContext.CreditManagementSystemReadWriteDbContext>(options =>
            {
                options.UseMySQL(connectionStrings);
            });

            var readWriteDbContextType = typeof(CreditManagementSystemDbContext.CreditManagementSystemReadWriteDbContext);
            var readOnlyDContextType = typeof(CreditManagementSystemDbContext.CreditManagementSystemReadOnlyDbContext);

            services.AddScoped<IUnitOfWork, CreditManagementSystemDbContext.CreditManagementSystemReadWriteDbContext>(provider =>
                provider.GetService<CreditManagementSystemDbContext.CreditManagementSystemReadWriteDbContext>());
            services.AddTransient<IIdGenerator, SequentialIdGenerator>();
            services.AddRepositories(readOnlyDContextType, readWriteDbContextType);
        }
    }

}
