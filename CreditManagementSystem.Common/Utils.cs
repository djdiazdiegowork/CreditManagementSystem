using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Extension;
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
        public static string GetNameOfSeedMethod => nameof(ISeed<IEntity>.SeedAsync);

        public static async Task ApplySeed(IServiceProvider provider)
        {
            var seedTypes = typeof(ISeed).GetEntityTypes();

            foreach (var seedType in seedTypes)
            {
                var entityType = seedType.BaseType.GenericTypeArguments;

                var parameters = new[] {
                    provider.GetService(typeof(IQueryRepository<>).MakeGenericType(entityType)),
                    provider.GetService(typeof(IRepository<>).MakeGenericType(entityType)),
                    provider.GetService(typeof(IUnitOfWork))
                };

                var methodInfo = seedType.GetMethod(GetNameOfSeedMethod);

                var instance = Activator.CreateInstance(seedType);

                var resultTask = (Task)methodInfo.Invoke(instance, parameters);

                await resultTask;
            }
        }

        public static List<Type> GetTypesFromAssembly()
        {
            var types = new List<Type>();

            var current = Directory.GetCurrentDirectory();

            var parent = Directory.GetParent(current);

            var directories = parent.GetDirectories().Where(d => d.Name.Contains(parent.Name));

            foreach (var directory in directories)
            {
                var assemblyTypes = Assembly.Load(directory.Name).GetTypes();

                foreach (var type in assemblyTypes)
                {
                    types.Add(type);
                }
            }

            return types;
        }

        public static ValueConverter<Guid, string> ConvertGuidToString()
        {
            return new ValueConverter<Guid, string>(u => u.ToString("N"), u => Guid.Parse(u));
        }
    }

}
