using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;

namespace Bubbio.Domain.Tests.Scenarios
{
    public class TestRepository : IRepository
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public Task<IEnumerable<IEvent>> GetEventsAsync(Guid babyId)
        {
            return Task.FromResult(_events
                .Where(e => e.BabyId.ToString().Equals(babyId.ToString())));
        }

        public Task<IEvent> GetLastEventAsync(Guid babyId, EventType eventType)
        {
            return Task.FromResult(_events
                .Where(e => e.BabyId.ToString().Equals(babyId.ToString()))
                .LastOrDefault(e => e.EventType.Equals(eventType)));
        }

        public void Has(IEvent @event)
        {
            _events.Add(@event);
        }

        public void IsEmpty()
        {
            _events.Clear();
        }
    }
}