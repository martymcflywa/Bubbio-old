using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Core.Exceptions;
using Bubbio.Domain.Validation;
using Xunit;

namespace Bubbio.Tests.Core
{
    public partial class TestRepository : IRepository
    {
        private readonly List<IEvent> _events = new List<IEvent>();
        private readonly IValidate _validator;

        public TestRepository(IValidate validator)
        {
            _validator = validator;
        }

        public Task BatchInsertAsync(IEnumerable<IEvent> events)
        {
            _events.AddRange(events);
            return Task.CompletedTask;
        }

        public Task InsertAsync(IEvent @event)
        {
            try
            {
                _events.Add(_validator.Validate(this, @event).Result);
            }
            catch (AggregateException e) when (e.GetBaseException() is InvalidEventException){}
            catch (InvalidEventException){}

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

        public void AlreadyHas(IEvent @event)
        {
            _events.Add(@event);
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
    }
}