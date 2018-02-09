using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Domain.Validation;
using Bubbio.Tests.Core;
using Xunit;

namespace Bubbio.Domain.Tests.Scenarios
{
    public class TransitionValidatorTestBase
    {
        private readonly TestRepository _testRepository = new TestRepository();
        private TransitionValidator Validator { get; set; }
        private bool IsAccepted { get; set; }
        private static readonly Guid BabyId = Guid.NewGuid();

        protected static BottleFeed Start => new BottleFeed
        {
            BabyId = BabyId,
            EventType = EventType.BottleFeed,
            Transition = Transition.Start
        };

        protected static BottleFeed End => new BottleFeed
        {
            BabyId = BabyId,
            EventType = EventType.BottleFeed,
            Transition = Transition.End
        };

        protected static TummyTime UnrelatedStart => new TummyTime
        {
            BabyId = BabyId,
            EventType = EventType.TummyTime,
            Transition = Transition.Start
        };

        protected static TummyTime UnrelatedEnd => new TummyTime
        {
            BabyId = BabyId,
            EventType = EventType.TummyTime,
            Transition = Transition.End
        };

        protected void RepositoryHas(IEvent @event)
        {
            _testRepository.Has(@event);
        }

        protected void RepositoryIsEmpty()
        {
            _testRepository.IsEmpty();
        }

        protected void EventIsValidated(ITransition @event)
        {
            Validator = new TransitionValidator(_testRepository);
            IsAccepted = Validator.IsValidAsync(@event).Result;
        }

        protected void EventIsAccepted(bool expected)
        {
            Assert.Equal(expected, IsAccepted);
        }
    }
}