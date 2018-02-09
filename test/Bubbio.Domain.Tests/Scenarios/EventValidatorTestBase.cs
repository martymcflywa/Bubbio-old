using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Domain.Validation;
using Bubbio.Tests.Core;
using Xunit;

namespace Bubbio.Domain.Tests.Scenarios
{
    public class EventValidatorTestBase
    {
        private readonly TestRepository _testRepository = new TestRepository();
        private EventValidator Validator { get; set; }

        private bool IsAccepted { get; set; }

        private readonly Guid _eventId = Guid.NewGuid();
        private readonly Guid _babyId = Guid.NewGuid();
        private readonly DateTimeOffset _timestamp = DateTimeOffset.Now;

        protected BottleFeed Start => new BottleFeed
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.BottleFeed,
            Transition = Transition.Start
        };

        protected BottleFeed End => new BottleFeed
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.BottleFeed,
            Transition = Transition.End
        };

        protected TummyTime UnrelatedStart => new TummyTime
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.TummyTime,
            Transition = Transition.Start
        };

        protected TummyTime UnrelatedEnd => new TummyTime
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.TummyTime,
            Transition = Transition.End
        };

        protected void RepositoryHas(IEvent @event)
        {
            _testRepository.InsertAsync(@event);
        }

        protected void RepositoryIsEmpty()
        {
            _testRepository.IsEmpty();
        }

        protected void EventIsValidated(IEvent @event)
        {
            Validator = new EventValidator(_testRepository);
            IsAccepted = Validator.IsValidAsync(@event).Result;
        }

        protected void EventIsAccepted(bool expected)
        {
            Assert.Equal(expected, IsAccepted);
        }
    }
}