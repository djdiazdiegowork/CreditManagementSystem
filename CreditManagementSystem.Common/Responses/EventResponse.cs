using CreditManagementSystem.Common.Data;
using System;

namespace CreditManagementSystem.Common.Responses
{
    public sealed class EventResponse<T> : IEventResponse
    {
        public EventResponse()
        {
        }

        public EventResponse(IEvent @event, int code, T body)
        {
            this.Code = code;
            this.Event = @event;
            this.Body = body;
        }

        public int Code { get; protected set; }

        public IEvent Event { get; protected set; }

        public T Body { get; protected set; }

        public bool IsSuccess
        {
            get
            {
                return 200 <= this.Code && this.Code <= 299;
            }
        }

        object IEventResponse.Body => this.Body;

        Type IEventResponse.GetBodyType()
        {
            T body = this.Body;
            ref T local = ref body;
            return local == null ? (Type)null : local.GetType();
        }
    }
}
