using Bubbio.WebApi.Tests.Scenarios;
using TestStack.BDDfy;
using Xunit;

namespace Bubbio.WebApi.Tests
{
    public class RepositoryControllerTests : RepositoryControllerTestBase
    {
        [Fact]
        public void UpdateEventsArePersisted()
        {
            this.Given(_ => EventToPersist(HeightUpdate))
                .When(_ => PersistIsRequested())
                .Then(_ => EventIsPersisted(HeightUpdate))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithoutPreviousEvents()
        {
            this.Given(_ => RepositoryIsEmpty())
                .And(_ => EventToPersist(StartTummyTime))
                .When(_ => PersistIsRequested())
                .Then(_ => EventIsPersisted(StartTummyTime))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithRelatedEndEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(EndTummyTime))
                .And(_ => EventToPersist(StartTummyTime))
                .When(_ => PersistIsRequested())
                .Then(_ => EventIsPersisted(StartTummyTime))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithUnrelatedEndEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(EndSleep))
                .And(_ => EventToPersist(StartTummyTime))
                .When(_ => PersistIsRequested())
                .Then(_ => EventIsPersisted(StartTummyTime))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithRelatedStartEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(StartTummyTime))
                .And(_ => EventToPersist(SecondStartTummyTime))
                .When(_ => PersistIsRequested())
                .Then(_ => EventNotPersisted(SecondStartTummyTime))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithUnrelatedStartEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(StartSleep))
                .And(_ => EventToPersist(StartTummyTime))
                .When(_ => PersistIsRequested())
                .Then(_ => EventIsPersisted(StartTummyTime))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithoutPreviousEvents()
        {
            this.Given(_ => RepositoryIsEmpty())
                .And(_ => EventToPersist(EndSleep))
                .When(_ => PersistIsRequested())
                .Then(_ => EventNotPersisted(EndSleep))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithRelatedEventEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(EndSleep))
                .And(_ => EventToPersist(SecondEndSleep))
                .When(_ => PersistIsRequested())
                .Then(_ => EventNotPersisted(SecondEndSleep))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithUnrelatedEndEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(EndBreastFeed))
                .And(_ => EventToPersist(EndSleep))
                .When(_ => PersistIsRequested())
                .Then(_ => EventNotPersisted(EndSleep))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithRelatedStartEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(StartSleep))
                .And(_ => EventToPersist(EndSleep))
                .When(_ => PersistIsRequested())
                .Then(_ => EventIsPersisted(EndSleep))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithUnrelatedStartEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(StartBreastFeed))
                .And(_ => EventToPersist(EndSleep))
                .When(_ => PersistIsRequested())
                .Then(_ => EventNotPersisted(EndSleep))
                .BDDfy();
        }
    }
}