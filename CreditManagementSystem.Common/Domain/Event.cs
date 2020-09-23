using CreditManagementSystem.Common.Data;

namespace CreditManagementSystem.Common.Domain
{
    public abstract class Event<Key> : IEvent
    {
        protected Event()
        {
        }

        protected Event(Key sourceID, bool isDomain, object body)
        {
            this.SourceID = sourceID;
            this.IsDomainEvent = isDomain;
            this.Body = body;
        }

        public Key SourceID { get; protected set; }

        public bool IsDomainEvent { get; protected set; }

        public object Body { get; protected set; }

        object IEvent.SourceID
        {
            get
            {
                return this.SourceID;
            }
        }
    }
}
