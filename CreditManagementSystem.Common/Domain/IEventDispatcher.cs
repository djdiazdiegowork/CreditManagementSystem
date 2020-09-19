using CreditManagementSystem.Common.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IEnumerable<IEvent> events);
    }
}
