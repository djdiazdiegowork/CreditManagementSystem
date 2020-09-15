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

            var pairs = (from type in types
                         let typesForService = new Dictionary<Type, Type>
                         {
                             { typeof(IRepository<>).MakeGenericType(type),
                                 typeof(Repository<,>).MakeGenericType(type, dbContextType)
                             },
                             { typeof(IQueryRepository<>).MakeGenericType(type),
                                 typeof(QueryRepository<,>).MakeGenericType(type, dbContextType)
                             }
                         }
                         from typeForService in typesForService
                         select (typeForService.Key, typeForService.Value)).ToArray();

            foreach (var (interfacetype, concreteType) in pairs)
            {
                services.AddScoped(interfacetype, provider => Activator.CreateInstance(concreteType, provider.GetService(dbContextType)));
            }
        }

        public static void AddServicesHandler(this IServiceCollection services, IEnumerable<Type> servicesTypes)
        {
            var pairs = (from service in servicesTypes
                         let referenceInterfaces = service.GetInterfaces()
                             .Where(p => p.GetInterfaces().Contains(typeof(IService)))
                         from referenceInterface in referenceInterfaces
                         select (referenceInterface, service)).ToArray();

            foreach (var (referenceInterface, service) in pairs)
            {
                services.AddScoped(referenceInterface, service);
            }
        }

        public static void AddCommandHandler(this IServiceCollection services, IEnumerable<Type> commandTypes)
        {
            var pairs = (from commandType in commandTypes
                         let genericInterfaceType = typeof(ICommandHandler<>).MakeGenericType(commandType)
                         let genericTypes = genericInterfaceType.GetEntityTypes()
                         from genericType in genericTypes
                         select (genericInterfaceType, genericType)).ToArray();

            foreach (var (genericInterfaceType, genericType) in pairs)
            {
                services.AddScoped(genericInterfaceType, genericType);
            }
        }
    }
}
