using System;
using System.Threading.Tasks;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Core.Exceptions;
using Bubbio.Domain.Validation;
using Bubbio.Tests.Core;
using Bubbio.WebApi.Controllers;
using Newtonsoft.Json;

namespace Bubbio.WebApi.Tests.Scenarios
{
    public class RepositoryControllerTestBase
    {
        private readonly TestRepository _repository;
        private readonly RepositoryController _controller;
        private string _eventToPersist;

        private readonly Guid _eventId = Guid.NewGuid();
        private readonly Guid _babyId = Guid.NewGuid();
        private readonly DateTimeOffset _timestamp = DateTimeOffset.Now;
        private const float Value = 7.1F;

        protected RepositoryControllerTestBase()
        {
            _repository = new TestRepository();
            IValidate validator = new TransitionValidator(_repository);
            _controller = new RepositoryController(_repository, validator);
        }

        protected void EventToPersist(IEvent @event) =>
            _eventToPersist = JsonConvert.SerializeObject(@event);

        protected async Task PersistIsRequested()
        {
            try
            {
                await _controller.InsertEventAsync(_eventToPersist);
            }
            catch (TransitionEventException){}
        }

        protected void EventIsPersisted(IEvent @event)
        {
            _repository.Has(@event);
        }

        protected void EventIsPersisted(ITransition @event)
        {
            _repository.Has(@event);
        }

        protected void EventNotPersisted(ITransition @event)
        {
            _repository.IsWithout(@event);
        }

        protected void RepositoryIsEmpty()
        {
            _repository.IsEmpty();
        }

        protected void RepositoryAlreadyHas(ITransition @event)
        {
            _repository.InsertAsync(@event);
        }

        protected HeightUpdate HeightUpdate => new HeightUpdate
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.HeightUpdate,
            Value = Value
        };

        protected TummyTime StartTummyTime => new TummyTime
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.TummyTime,
            Transition = Transition.Start
        };

        protected TummyTime SecondStartTummyTime => new TummyTime
        {
            SequenceId = 2,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.TummyTime,
            Transition = Transition.Start
        };

        protected TummyTime EndTummyTime => new TummyTime
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.TummyTime,
            Transition = Transition.End
        };

        protected Sleep StartSleep => new Sleep
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.Sleep,
            Transition = Transition.Start
        };

        protected Sleep EndSleep => new Sleep
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.Sleep,
            Transition = Transition.End
        };

        protected Sleep SecondEndSleep => new Sleep
        {
            SequenceId = 2,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.Sleep,
            Transition = Transition.End
        };

        protected BreastFeed StartBreastFeed => new BreastFeed
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.BreastFeed,
            Transition = Transition.Start
        };

        protected BreastFeed EndBreastFeed => new BreastFeed
        {
            SequenceId = 1,
            EventId = _eventId,
            BabyId = _babyId,
            Timestamp = _timestamp,
            EventType = EventType.BreastFeed,
            Transition = Transition.End
        };
    }
}