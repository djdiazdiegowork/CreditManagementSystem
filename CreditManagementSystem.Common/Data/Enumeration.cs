using System;

namespace CreditManagementSystem.Common.Data
{
    public abstract class Enumeration<TKey> : Entity<TKey>, IEnumeration where TKey : Enum
    {
        protected Enumeration()
        {
        }

        public string Name { get; protected set; }

        public int CompareTo(object other)
        {
            return this.ID.CompareTo((object)((Entity<TKey>)other).ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration<TKey> enumeration))
                return false;

            bool flag = this.ID.Equals((object)enumeration.ID);

            return this.GetType().Equals(obj.GetType()) & flag;
        }
    }
}
