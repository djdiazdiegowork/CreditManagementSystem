using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Response;

namespace CreditManagementSystem.Common.Extension
{
    public static class IEventExtension
    {
        public static IEventResponse OkResponse<T>(
            this IEvent @event,
            T body,
            bool skipEventInfo = true)
        {
            return new EventResponse<T>(skipEventInfo ? (IEvent)null : @event, 200, body);
        }

        public static IEventResponse ErrorResponse<T>(
            this IEvent @event,
            T body,
            bool skipCommandInfo = true)
        {
            return new EventResponse<T>(skipCommandInfo ? (IEvent)null : @event, 500, body);
        }
    }
}
