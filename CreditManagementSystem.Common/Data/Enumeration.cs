using System;

namespace CreditManagementSystem.Common.Data
{
    public abstract class Enumeration<TKey> : Entity<TKey>, IEnumeration where TKey : Enum
    {
        public string Name { get; set; }
    }
}
