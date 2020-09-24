using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain
{
    public interface IEventDispatcher
    {
        Task<CombinedEventResponse> DispatchAsync(IEnumerable<IEvent> events);
    }
}
