using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Domain.Handler;
using CreditManagementSystem.Common.Extension;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CreditManagementSystem.Domain.Handler.DependencyInjection
{
    public static class IServiceCollectionExtenssion
    {
        public static void AddServicesAndCommands(this IServiceCollection services)
        {
            AddNetflixWebApiServicesHandler(services);
            AddNetflixWebApiCommandHandler(services);

            services.AddScoped<ICommandDispatcher, CommadDispatcher>();
        }

        private static void AddNetflixWebApiServicesHandler(IServiceCollection services)
        {
            var baseType = typeof(IService);

            var existServices = baseType.GetEntityTypes();

            foreach (var service in existServices)
            {
                var referenceInterfaces = service
                    .GetInterfaces()
                    .Where(p => p.GetInterfaces().Contains(baseType));

                foreach (var @interface in referenceInterfaces)
                    services.AddScoped(@interface, service);
            }
        }

        private static void AddNetflixWebApiCommandHandler(IServiceCollection services)
        {
            var commandTypes = typeof(ICommand).GetEntityTypes();

            var baseCommandHandlerType = typeof(ICommandHandler<>);

            foreach (var commandType in commandTypes)
            {
                var baseGenericCommandHandlerType = baseCommandHandlerType.MakeGenericType(commandType);

                var commandHandlerTypes = baseGenericCommandHandlerType.GetEntityTypes();

                foreach (var commandHandlerType in commandHandlerTypes)
                {
                    services.AddScoped(baseGenericCommandHandlerType, commandHandlerType);
                }
            }
        }
    }

}
