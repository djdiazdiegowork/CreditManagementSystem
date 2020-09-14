using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Domain.Handler;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditManagementSystem.Common.Extension
{
    public static class IServiceCollectionExtension
    {
        public static void AddServicesHandler(this IServiceCollection services, IEnumerable<Type> servicesTypes)
        {
            foreach (var service in servicesTypes)
            {
                var referenceInterfaces = service
                    .GetInterfaces()
                    .Where(p => p.GetInterfaces().Contains(typeof(IService)));

                foreach (var referenceInterface in referenceInterfaces)
                    services.AddScoped(referenceInterface, service);
            }
        }

        public static void AddCommandHandler(this IServiceCollection services, IEnumerable<Type> commandTypes)
        {
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
