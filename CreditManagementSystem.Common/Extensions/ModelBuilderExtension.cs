using CreditManagementSystem.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CreditManagementSystem.Common.Extensions
{
    public class F : IMutableEntityType
    {
        public object this[string name] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        object IAnnotatable.this[string name] => throw new System.NotImplementedException();

        public IMutableModel Model => throw new System.NotImplementedException();

        public IMutableEntityType BaseType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IMutableEntityType DefiningEntityType => throw new System.NotImplementedException();

        public bool IsKeyless { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public string DefiningNavigationName => throw new System.NotImplementedException();

        public string Name => throw new System.NotImplementedException();

        public System.Type ClrType => throw new System.NotImplementedException();

        IEntityType IEntityType.BaseType => throw new System.NotImplementedException();

        IEntityType IEntityType.DefiningEntityType => throw new System.NotImplementedException();

        IModel ITypeBase.Model => throw new System.NotImplementedException();

        public IAnnotation AddAnnotation(string name, object value)
        {
            throw new System.NotImplementedException();
        }

        public IMutableForeignKey AddForeignKey(IReadOnlyList<IMutableProperty> properties, IMutableKey principalKey, IMutableEntityType principalEntityType)
        {
            throw new System.NotImplementedException();
        }

        public void AddIgnored(string memberName)
        {
            throw new System.NotImplementedException();
        }

        public IMutableIndex AddIndex(IReadOnlyList<IMutableProperty> properties)
        {
            throw new System.NotImplementedException();
        }

        public IMutableKey AddKey(IReadOnlyList<IMutableProperty> properties)
        {
            throw new System.NotImplementedException();
        }

        public IMutableProperty AddProperty(string name, System.Type propertyType, MemberInfo memberInfo)
        {
            throw new System.NotImplementedException();
        }

        public IMutableServiceProperty AddServiceProperty(MemberInfo memberInfo)
        {
            throw new System.NotImplementedException();
        }

        public IAnnotation FindAnnotation(string name)
        {
            throw new System.NotImplementedException();
        }

        public IMutableForeignKey FindForeignKey(IReadOnlyList<IProperty> properties, IKey principalKey, IEntityType principalEntityType)
        {
            throw new System.NotImplementedException();
        }

        public IMutableIndex FindIndex(IReadOnlyList<IProperty> properties)
        {
            throw new System.NotImplementedException();
        }

        public IMutableKey FindKey(IReadOnlyList<IProperty> properties)
        {
            throw new System.NotImplementedException();
        }

        public IMutableKey FindPrimaryKey()
        {
            throw new System.NotImplementedException();
        }

        public IMutableProperty FindProperty(string name)
        {
            throw new System.NotImplementedException();
        }

        public IMutableServiceProperty FindServiceProperty(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IAnnotation> GetAnnotations()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMutableForeignKey> GetForeignKeys()
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<string> GetIgnoredMembers()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMutableIndex> GetIndexes()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMutableKey> GetKeys()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMutableProperty> GetProperties()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMutableServiceProperty> GetServiceProperties()
        {
            throw new System.NotImplementedException();
        }

        public bool IsIgnored(string memberName)
        {
            throw new System.NotImplementedException();
        }

        public IAnnotation RemoveAnnotation(string name)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveForeignKey(IMutableForeignKey foreignKey)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveIgnored(string memberName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveIndex(IMutableIndex index)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveKey(IMutableKey key)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveProperty(IMutableProperty property)
        {
            throw new System.NotImplementedException();
        }

        public IMutableServiceProperty RemoveServiceProperty(string name)
        {
            throw new System.NotImplementedException();
        }

        public void SetAnnotation(string name, object value)
        {
            throw new System.NotImplementedException();
        }

        public IMutableKey SetPrimaryKey(IReadOnlyList<IMutableProperty> properties)
        {
            throw new System.NotImplementedException();
        }

        IForeignKey IEntityType.FindForeignKey(IReadOnlyList<IProperty> properties, IKey principalKey, IEntityType principalEntityType)
        {
            throw new System.NotImplementedException();
        }

        IIndex IEntityType.FindIndex(IReadOnlyList<IProperty> properties)
        {
            throw new System.NotImplementedException();
        }

        IKey IEntityType.FindKey(IReadOnlyList<IProperty> properties)
        {
            throw new System.NotImplementedException();
        }

        IKey IEntityType.FindPrimaryKey()
        {
            throw new System.NotImplementedException();
        }

        IProperty IEntityType.FindProperty(string name)
        {
            throw new System.NotImplementedException();
        }

        IServiceProperty IEntityType.FindServiceProperty(string name)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<IForeignKey> IEntityType.GetForeignKeys()
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<IIndex> IEntityType.GetIndexes()
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<IKey> IEntityType.GetKeys()
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<IProperty> IEntityType.GetProperties()
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<IServiceProperty> IEntityType.GetServiceProperties()
        {
            throw new System.NotImplementedException();
        }
    }

    public static class ModelBuilderExtension
    {
        public static void AddEntities(this ModelBuilder modelBuilder)
        {
            var types = typeof(IEntity).GetEntityTypes();

            foreach (var type in types)
            {
                modelBuilder.Entity(type);
            }

            var assemblyTypes = Utils.GetTypesFromAssembly();

            var baseType = assemblyTypes
                .Where(t => t.IsClass && t.IsAbstract && t.IsGenericType
                    && t.IsTypeDefinition && t.GetInterfaces().Any(i => i.IsGenericType
                        && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .First();

            modelBuilder.ApplyConfigurationsFromAssembly(baseType.Assembly, t => t.IsClass && !t.IsAbstract);
        }
    }
}
