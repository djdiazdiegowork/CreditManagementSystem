namespace CreditManagementSystem.Common.Data
{
    public interface IEvent
    {
        object SourceID { get; }

        bool IsDomainEvent { get; }

        object Body { get; }
    }
}
