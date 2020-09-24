using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Extensions;
using CreditManagementSystem.Common.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain.Handler
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _provider;
        public EventDispatcher(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public Task<CombinedEventResponse> DispatchAsync(IEnumerable<IEvent> events)
        {
            List<Task<IEventResponse>> tasks = new List<Task<IEventResponse>>();

            foreach (var @event in events)
            {
                try
                {
                    var eventHandler = this._provider.GetService(
                         typeof(IEventHandler<>).MakeGenericType(@event.GetType()));

                    var methodInfo = eventHandler.GetType().GetMethod(nameof(IEventHandler<IEvent>.HandleAsync));

                    var resultTask = ((Task<IEventResponse>)methodInfo.Invoke(eventHandler, new[] { @event }))
                            .ContinueWith(c => c.Exception != null ? @event.ErrorResponse(c.Exception, false) : c.Result);

                    tasks.Add(resultTask);
                }
                catch (Exception ex)
                {
                    tasks.Add(Task.FromResult(@event.ErrorResponse(ex, false)));
                }
            }

            return Task.WhenAll(tasks).ContinueWith(evt => new CombinedEventResponse(evt.Result));
        }
    }
}
