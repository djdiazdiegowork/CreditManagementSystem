using CreditManagementSystem.Common.Domain;
using System;

namespace CreditManagementSystem.Domain.CommandCredit.Event
{
    public sealed class CreditCreateEvent : Event<Guid>
    {
        public CreditCreateEvent(Guid sourceID, CreditCreateCommand cmd)
            : base(sourceID, true, cmd)
        {
        }
    }
}
