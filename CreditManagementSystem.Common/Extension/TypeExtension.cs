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

            var types = !(type.IsGenericType && type.IsTypeDefinition) ?
                assemblyTypes.Where(t => t.IsClass && !t.IsAbstract &&
                    t.GetInterfaces().Contains(type)) :
                assemblyTypes.Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type));

            return types.ToArray();
        }
    }
}
