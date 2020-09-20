using CreditManagementSystem.Common.Data;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
