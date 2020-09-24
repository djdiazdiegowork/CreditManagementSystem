using System;

namespace CreditManagementSystem.Common.Data
{
    public abstract class Entity<TKey> : IEntity
    {
        private int? _requestedHashCode;

        public TKey ID { get; protected set; }

        public bool IsTransient()
        {
            return (typeof(TKey) == typeof(long) || typeof(TKey) == typeof(int) || typeof(TKey) == typeof(Guid)) && this.ID.Equals((object)default(TKey));
        }

        public override int GetHashCode()
        {
            if (this.IsTransient())
                return base.GetHashCode();
            if (!this._requestedHashCode.HasValue)
                this._requestedHashCode = new int?(this.ID.GetHashCode() ^ 31);
            return this._requestedHashCode.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
                return false;
            if (this == obj)
                return true;
            if (this.GetType() != obj.GetType())
                return false;
            Entity<TKey> entity = (Entity<TKey>)obj;
            return !entity.IsTransient() && !this.IsTransient() && entity.ID.Equals((object)this.ID);
        }

        object IEntity.ID => this.ID;
    }
}
