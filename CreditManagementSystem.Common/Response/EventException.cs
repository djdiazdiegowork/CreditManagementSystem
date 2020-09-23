using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CreditManagementSystem.Common.Response
{
    public class EventException : Exception
    {
        public IEnumerable<IEventResponse> Errors { get; private set; }

        public EventException(string message)
          : this(message, Enumerable.Empty<IEventResponse>())
        {
        }

        public EventException(string message, IEnumerable<IEventResponse> errors)
          : base(message)
        {
            this.Errors = errors;
        }

        public EventException(IEnumerable<IEventResponse> errors)
          : base()
        {
            this.Errors = errors;
        }

        public EventException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
            this.Errors = info.GetValue("errors", typeof(IEnumerable<IEventResponse>)) as IEnumerable<IEventResponse>;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            info.AddValue("errors", this.Errors);
            base.GetObjectData(info, context);
        }
    }
}
