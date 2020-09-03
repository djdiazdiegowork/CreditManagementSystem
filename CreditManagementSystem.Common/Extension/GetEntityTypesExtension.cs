using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace CreditManagementSystem.Common.Extension
{
    public static class GetEntityTypesExtension
    {
        public static IEnumerable<Type> GetEntityTypes(this Type type)
        {
            var assemblyTypes = Utils.GetTypesFromAssembly();

            var types = assemblyTypes.Where(t => !t.IsAbstract && t.IsClass &&
                        t.GetInterfaces().Contains(type));

            return types;
        }
    }
}
