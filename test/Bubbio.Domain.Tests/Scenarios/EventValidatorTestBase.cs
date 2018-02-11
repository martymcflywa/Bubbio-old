using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Core.Exceptions;
using Bubbio.Domain.Validation;
using Bubbio.Tests.Core;
using Xunit;

using Name = Bubbio.Core.Events.Name;

namespace Bubbio.Domain.Tests.Scenarios
{
    public class EventValidatorTestBase
    {
        private readonly EventValidator _validator;
        private readonly TestRepository _testRepository;

        private readonly Guid _eventId = Guid.NewGuid();
        private readonly Guid _babyId = Guid.NewGuid();
        private readonly DateTimeOffset _timestamp = DateTimeOffset.Now;

        private IEvent ValidatedEvent { get; set; }

        protected EventValidatorTestBase()
        {
            _validator = new EventValidator();
            _testRepository = new TestRepository(_validator);
        }

        protected CreateBaby BabyWithValidName => new CreateBaby
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.CreateBaby,
            Name = new Name
            {
                First = "Damon",
                Last = "Ponce"
            }
        };

        protected CreateBaby BabyWithInvalidName => new CreateBaby
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.CreateBaby,
            Name = new Name
            {
                First = "L33t",
                Last = "123456"
            }
        };

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
                ValidatedEvent = _validator.Validate(_testRepository, @event).Result;
            }
            catch (AggregateException e) when (e.GetBaseException() is InvalidEventException){}
            catch (InvalidEventException){}
        }

        protected void EventIsAccepted(IEvent @event)
        {
            Assert.Equal(@event, ValidatedEvent, new RootEventComparer());
        }

        protected void EventNotAccepted()
        {
            Assert.Null(ValidatedEvent);
        }
    }
}