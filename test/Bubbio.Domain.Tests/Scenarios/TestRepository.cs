using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;

namespace Bubbio.Domain.Tests.Scenarios
{
    public class TestRepository : IRepository
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public Task BatchInsertAsync(IEnumerable<IEvent> events)
        {
            _events.AddRange(events);
            return Task.CompletedTask;
        }

        public Task InsertAsync(IEvent @event)
        {
            _events.Add(@event);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<IEvent>> BatchGetAsync(Guid babyId)
        {
            return Task.FromResult(_events
                .Where(e => e.BabyId.Equals(babyId)));
        }

        public Task<IEvent> GetLastAsync(Guid babyId, EventType eventType)
        {
            return Task.FromResult(_events
                .Where(e => e.BabyId.Equals(babyId))
                .LastOrDefault(e => e.EventType.Equals(eventType)));
        }

        public void Has(IEvent @event)
        {
            InsertAsync(@event);
        }

        public void Has(IEnumerable<IEvent> events)
        {
            BatchInsertAsync(events);
        }

        public void IsEmpty()
        {
            _events.Clear();
        }
    }
}