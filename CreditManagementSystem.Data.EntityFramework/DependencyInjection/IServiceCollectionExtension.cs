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
            services.AddDbContext<CreditManagementSystemDbContext>(options =>
            {
                options.UseMySQL(connectionStrings);
            });

            var dbContextType = typeof(CreditManagementSystemDbContext);

            services.AddScoped(typeof(IUnitOfWork), typeof(CreditManagementSystemDbContext));
            services.AddTransient<IIdGenerator, SequentialIdGenerator>();
            services.AddRepositories(dbContextType);
        }
    }

}
