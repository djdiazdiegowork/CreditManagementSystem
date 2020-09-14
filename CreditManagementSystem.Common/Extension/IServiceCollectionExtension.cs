using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Data.EntityFramework;
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
        public static void AddRepositories(this IServiceCollection services, Type dbContextType)
        {
            var types = typeof(IEntity).GetEntityTypes();

            foreach (var type in types)
            {
                var typesForService = new Dictionary<Type, Type> {
                    { typeof(IRepository<>).MakeGenericType(type), typeof(Repository<,>).MakeGenericType(type, dbContextType)  },
                    { typeof(IQueryRepository<>).MakeGenericType(type), typeof(QueryRepository<,>).MakeGenericType(type, dbContextType)}
                };

                foreach (var typeForService in typesForService)
                {
                    AddService(services, typeForService.Key, typeForService.Value, dbContextType);
                }
            }
        }

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

        private static void AddService(IServiceCollection services, Type interfacetype, Type concreteType, Type dbContextType)
        {
            services.AddScoped(interfacetype, provider => Activator.CreateInstance(concreteType, provider.GetService(dbContextType)));
        }
    }
}
