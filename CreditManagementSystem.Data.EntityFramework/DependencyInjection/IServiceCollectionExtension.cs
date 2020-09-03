using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Extension;
using CreditManagementSystem.Common.SequentialGuidGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CreditManagementSystem.Data.EntityFramework.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static void AddUseMySqlServer(this IServiceCollection services, string connectionStrings)
        {
            services.AddDbContext<CreditManagementSystemDbContext>(options => {
                options.UseMySQL(connectionStrings);
            });

            var dbContextType = typeof(CreditManagementSystemDbContext);

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<>).MakeGenericType(dbContextType));
            services.AddTransient<IIdGenerator, SequentialIdGenerator>();
            AddNetflixWebApiRepositories(services, dbContextType);
        }

        private static void AddNetflixWebApiRepositories(IServiceCollection services, Type dbContextType)
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

        private static void AddService(IServiceCollection services, Type interfacetype, Type concreteType, Type dbContextType)
        {
            services.AddScoped(interfacetype, provider => Activator.CreateInstance(concreteType, provider.GetService(dbContextType)));
        }
    }

}
