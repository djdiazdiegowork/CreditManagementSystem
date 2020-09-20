using CreditManagementSystem.Common.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain.Handler
{
    public abstract class EventDispatcher : IEventDispatcher
    {
        public Task DispatchAsync(IEnumerable<IEvent> events)
        {
            throw new System.NotImplementedException();
        }
    }
}
