using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Domain.Handler;
using CreditManagementSystem.Common.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace CreditManagementSystem.Domain.Handler.DependencyInjection
{
    public static class IServiceCollectionExtenssion
    {
        public static void AddServicesAndCommands(this IServiceCollection services)
        {
            services.AddServicesHandler(typeof(IService).GetEntityTypes());
            services.AddCommandHandler(typeof(ICommand).GetEntityTypes());
            services.AddScoped<ICommandDispatcher, CommadDispatcher>();
        }
    }
}
