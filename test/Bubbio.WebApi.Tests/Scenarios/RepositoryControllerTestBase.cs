using System;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
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
            await _controller.InsertEventAsync(_eventToPersist);
        }

        protected void EventIsPersisted(IEvent @event)
        {
            _repository.Has(@event);
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
    }
}