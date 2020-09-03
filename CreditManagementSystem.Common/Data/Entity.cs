namespace CreditManagementSystem.Common.Data
{
    public abstract class Entity<TKey> : IEntity
    {
        public TKey ID { get; set; }
        object IEntity.ID => this.ID;
    }
}
