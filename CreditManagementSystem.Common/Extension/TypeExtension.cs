using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditManagementSystem.Common.Extension
{
    public static class TypeExtension
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
