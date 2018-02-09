using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Xunit;

namespace Bubbio.Tests.Core
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
            Assert.Equal(@event, _events.LastOrDefault(), new RootEventComparer());
        }

        public void Has(ITransition @event)
        {
            Assert.Equal(@event, _events.LastOrDefault() as ITransition, new TransitionEventComparer());
        }

        public void IsWithout(IEvent @event)
        {
            Assert.NotEqual(@event, _events.LastOrDefault(), new RootEventComparer());
        }

        public void IsEmpty()
        {
            _events.Clear();
        }

        private class RootEventComparer : IEqualityComparer<IEvent>
        {
            public bool Equals(IEvent x, IEvent y)
            {
                if (x == null || y == null)
                    return false;

                return x.SequenceId.Equals(y.SequenceId)
                       && x.EventId.ToString().Equals(y.EventId.ToString())
                       && x.BabyId.ToString().Equals(y.BabyId.ToString())
                       && x.EventType.Equals(y.EventType)
                       && x.Timestamp.Equals(y.Timestamp);
            }

            public int GetHashCode(IEvent obj)
            {
                throw new NotImplementedException();
            }
        }

        private class TransitionEventComparer : IEqualityComparer<ITransition>
        {
            public bool Equals(ITransition x, ITransition y)
            {
                return x.SequenceId.Equals(y.SequenceId)
                       && x.EventId.ToString().Equals(y.EventId.ToString())
                       && x.BabyId.ToString().Equals(y.BabyId.ToString())
                       && x.EventType.Equals(y.EventType)
                       && x.Timestamp.Equals(y.Timestamp)
                       && x.Transition.Equals(y.Transition);
            }

            public int GetHashCode(ITransition obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}