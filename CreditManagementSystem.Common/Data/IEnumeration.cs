using System;

namespace CreditManagementSystem.Common.Data
{
    public interface IEnumeration : IEntity, IComparable
    {
        string Name { get; }
    }
}
