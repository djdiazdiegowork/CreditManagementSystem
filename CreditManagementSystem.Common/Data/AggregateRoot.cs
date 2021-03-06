﻿using System.Collections.Generic;

namespace CreditManagementSystem.Common.Data
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {
        private readonly List<IEvent> _events;

        protected AggregateRoot()
        {
            this._events = new List<IEvent>();
        }

        public void ClearEvents()
        {
            this._events.Clear();
        }

        public IReadOnlyCollection<IEvent> GetEvents()
        {
            return this._events;
        }

        protected void AddEvent<TEvent>(TEvent @event) where TEvent : IEvent
        {
            this._events.Add(@event);
        }
    }
}
