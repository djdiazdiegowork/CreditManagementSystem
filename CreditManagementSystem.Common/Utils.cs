using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Data.EntityFramework;
using CreditManagementSystem.Common.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common
{
    public static class Utils
    {
        public static async Task ApplySeed(IServiceProvider provider)
        {
            var seedTypes = typeof(ISeed<>).GetEntityTypes();

            var tasks = (from seedType in seedTypes
                         let entityType = seedType.BaseType.GenericTypeArguments
                         let parameters = new[]
                         {
                                   provider.GetService(typeof(IQueryRepository<>).MakeGenericType(entityType)),
                                   provider.GetService(typeof(IRepository<>).MakeGenericType(entityType)),
                                   provider.GetService(typeof(IUnitOfWork))
                               }
                         let methodInfo = seedType.GetMethod(nameof(ISeed<IEntity>.SeedAsync))
                         let instance = Activator.CreateInstance(seedType)
                         let resultTask = (Task)methodInfo.Invoke(instance, parameters)
                         select resultTask).ToArray();

            foreach (var task in tasks)
            {
                await task;
            }
        }

        public static async Task ApplyPenndingMigrations(DbContext dbContext)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
                await dbContext.Database.MigrateAsync();
        }

        public static IEnumerable<Type> GetTypesFromAssembly()
        {
            var current = Directory.GetCurrentDirectory();
            var parent = Directory.GetParent(current);
            var directories = parent.GetDirectories().Where(d => d.Name.Contains(parent.Name)).ToArray();

            return (from directory in directories
                    let assemblyTypes = Assembly.Load(directory.Name).GetTypes()
                    from type in assemblyTypes
                    select type).ToArray();
        }

        public static ValueConverter<Guid, string> ConvertGuidToString()
        {
            return new ValueConverter<Guid, string>(u => u.ToString("N"), u => Guid.Parse(u));
        }
    }
}
