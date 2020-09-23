using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Extension;
using CreditManagementSystem.Domain.CommandCredit.Event;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Handler.CommandCredit.Event
{
    public class CreditCreateEventHandler : IEventHandler<CreditCreateEvent>
    {
        public Task HandleAsync(CreditCreateEvent @event)
        {
            return Task.FromResult(@event.OkResponse(true));
        }
    }
}
