using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Core.Exceptions;
using Bubbio.Domain.Validation;
using Bubbio.Tests.Core;
using Xunit;

namespace Bubbio.Domain.Tests.Scenarios
{
    public class EventValidatorTestBase
    {
        private readonly EventValidator _validator;
        private readonly TestRepository _testRepository;

        private readonly Guid _eventId = Guid.NewGuid();
        private readonly Guid _babyId = Guid.NewGuid();
        private readonly DateTimeOffset _timestamp = DateTimeOffset.Now;

        private IEvent _validatedEvent { get; set; }

        protected EventValidatorTestBase()
        {
            _validator = new EventValidator();
            _testRepository = new TestRepository(_validator);
        }

        protected BottleFeed Start => new BottleFeed
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.BottleFeed,
            Transition = Transition.Start
        };

        protected BottleFeed SecondStart => new BottleFeed
        {
            SequenceId = 2,
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

        protected BottleFeed SecondEnd => new BottleFeed
        {
            SequenceId = 2,
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

        protected void RepositoryAlreadyHas(IEvent @event)
        {
            _testRepository.AlreadyHas(@event);
        }

        protected void RepositoryIsEmpty()
        {
            _testRepository.IsEmpty();
        }

        protected void EventIsValidated(IEvent @event)
        {
            try
            {
                _validatedEvent = _validator.Validate(_testRepository, @event).Result;
            }
            catch (AggregateException e) when (e.GetBaseException() is InvalidEventException){}
            catch (InvalidEventException){}
        }

        protected void EventIsAccepted(IEvent @event)
        {
            Assert.Equal(@event, _validatedEvent, new RootEventComparer());
        }

        protected void EventNotAccepted()
        {
            Assert.Null(_validatedEvent);
        }
    }
}