using System;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Domain.Tests.Scenarios;
using Bubbio.Domain.Validation;
using TestStack.BDDfy;
using Xunit;

namespace Bubbio.Domain.Tests
{
    public class TransitionValidatorTests
    {
        [Fact]
        public void StartEventWithRelatedEndEvent()
        {
            this.Given(_ => RepositoryHas(End))
                .When(_ => EventIsValidated(Start))
                .Then(_ => EventIsAccepted(true))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithoutPreviousEvents()
        {
            this.Given(_ => RepositoryIsEmpty())
                .When(_ => EventIsValidated(Start))
                .Then(_ => EventIsAccepted(true))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithUnrelatedEndEvent()
        {
            this.Given(_ => RepositoryHas(UnrelatedEnd))
                .When(_ => EventIsValidated(Start))
                .Then(_ => EventIsAccepted(true))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithRelatedStartEvent()
        {
            this.Given(_ => RepositoryHas(Start))
                .When(_ => EventIsValidated(Start))
                .Then(_ => EventIsAccepted(false))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithRelatedStartEvent()
        {
            this.Given(_ => RepositoryHas(Start))
                .When(_ => EventIsValidated(End))
                .Then(_ => EventIsAccepted(true))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithoutPreviousEvents()
        {
            this.Given(_ => RepositoryIsEmpty())
                .When(_ => EventIsValidated(End))
                .Then(_ => EventIsAccepted(false))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithUnrelatedStartEvent()
        {
            this.Given(_ => RepositoryHas(UnrelatedStart))
                .When(_ => EventIsValidated(End))
                .Then(_ => EventIsAccepted(false))
                .BDDfy();
        }

        private readonly TestRepository _testRepository = new TestRepository();
        private TransitionValidator Validator { get; set; }
        private bool IsAccepted { get; set; }
        private static readonly Guid BabyId = Guid.NewGuid();

        private static BottleFeedEvent Start => new BottleFeedEvent
        {
            BabyId = BabyId,
            EventType = EventType.Feed,
            Transition = Transition.Start
        };

        private static BottleFeedEvent End => new BottleFeedEvent
        {
            BabyId = BabyId,
            EventType = EventType.Feed,
            Transition = Transition.End
        };

        private static TummyTimeEvent UnrelatedStart => new TummyTimeEvent
        {
            BabyId = BabyId,
            EventType = EventType.TummyTime,
            Transition = Transition.Start
        };

        private static TummyTimeEvent UnrelatedEnd => new TummyTimeEvent
        {
            BabyId = BabyId,
            EventType = EventType.TummyTime,
            Transition = Transition.End
        };

        private void RepositoryHas(IEvent @event)
        {
            _testRepository.Has(@event);
        }

        private void RepositoryIsEmpty()
        {
            _testRepository.IsEmpty();
        }

        private void EventIsValidated(ITransitionEvent @event)
        {
            Validator = new TransitionValidator(_testRepository);
            IsAccepted = Validator.IsValidAsync(@event).Result;
        }

        private void EventIsAccepted(bool expected)
        {
            Assert.Equal(expected, IsAccepted);
        }
    }
}