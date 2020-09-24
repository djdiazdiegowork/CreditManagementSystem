using System.Collections.Generic;

namespace CreditManagementSystem.Common.Response
{
    public class CombinedEventResponse
    {
        private readonly List<IEventResponse> _eventsResponseFail;
        private readonly List<IEventResponse> _eventsResponsesSuccess;

        public CombinedEventResponse(IEventResponse[] eventResponses)
        {
            this._eventsResponseFail = new List<IEventResponse>();
            this._eventsResponsesSuccess = new List<IEventResponse>();

            foreach (var eventResponse in eventResponses)
            {
                if (!eventResponse.IsSuccess)
                {
                    this._eventsResponseFail.Add(eventResponse);
                    continue;
                }
                this._eventsResponsesSuccess.Add(eventResponse);
            }
        }

        public IReadOnlyCollection<IEventResponse> GetEventsResponseFail()
        {
            return this._eventsResponseFail;
        }

        public IReadOnlyCollection<IEventResponse> GetEventsResponseSuccess()
        {
            return this._eventsResponsesSuccess;
        }
    }
}
