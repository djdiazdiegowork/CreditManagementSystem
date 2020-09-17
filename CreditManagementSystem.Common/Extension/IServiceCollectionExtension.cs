﻿using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Data.EntityFramework;
using CreditManagementSystem.Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditManagementSystem.Common.Extension
{
    public static class IServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services, Type readOnlyDbContextType, Type readWriteDbContextType)
        {
            var types = typeof(IEntity).GetEntityTypes();

            foreach (var type in types)
            {
                CreateRepositories(services, type, typeof(IRepository<>), typeof(Repository<,>), readWriteDbContextType);
                CreateRepositories(services, type, typeof(IQueryRepository<>), typeof(QueryRepository<,>), readOnlyDbContextType);
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

        private static void CreateRepositories(IServiceCollection services, Type entityType, Type interfacetype, Type concreteType, Type dbContextType)
        {
            interfacetype = interfacetype.MakeGenericType(entityType);
            concreteType = concreteType.MakeGenericType(entityType, dbContextType);

            services.AddScoped(interfacetype, provider => Activator.CreateInstance(concreteType, provider.GetService(dbContextType)));
        }
    }
}
