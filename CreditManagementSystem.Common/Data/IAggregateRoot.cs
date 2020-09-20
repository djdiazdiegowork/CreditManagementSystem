using System;
using System.Collections.Generic;
using System.Text;

namespace CreditManagementSystem.Common.Data
{
    public interface IAggregateRoot
    {
        void ClearEvents();

        IReadOnlyCollection<IEvent> GetEvents();
    }
}
