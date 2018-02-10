using Bubbio.Domain.Tests.Scenarios;
using TestStack.BDDfy;
using Xunit;

namespace Bubbio.Domain.Tests
{
    public class EventValidatorTests : EventValidatorTestBase
    {
        [Fact]
        public void StartEventWithRelatedEndEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(End))
                .When(_ => EventIsValidated(Start))
                .Then(_ => EventIsAccepted(Start))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithoutPreviousEvents()
        {
            this.Given(_ => RepositoryIsEmpty())
                .When(_ => EventIsValidated(Start))
                .Then(_ => EventIsAccepted(Start))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithUnrelatedEndEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(UnrelatedEnd))
                .When(_ => EventIsValidated(Start))
                .Then(_ => EventIsAccepted(Start))
                .BDDfy();
        }

        [Fact]
        public void StartEventWithRelatedStartEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(Start))
                .When(_ => EventIsValidated(SecondStart))
                .Then(_ => EventNotAccepted())
                .BDDfy();
        }

        [Fact]
        public void EndEventWithRelatedStartEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(Start))
                .When(_ => EventIsValidated(End))
                .Then(_ => EventIsAccepted(End))
                .BDDfy();
        }

        [Fact]
        public void EndEventWithoutPreviousEvents()
        {
            this.Given(_ => RepositoryIsEmpty())
                .When(_ => EventIsValidated(End))
                .Then(_ => EventNotAccepted())
                .BDDfy();
        }

        [Fact]
        public void EndEventWithUnrelatedStartEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(UnrelatedStart))
                .When(_ => EventIsValidated(End))
                .Then(_ => EventNotAccepted())
                .BDDfy();
        }

        [Fact]
        public void EndEventWithRelatedEndEvent()
        {
            this.Given(_ => RepositoryAlreadyHas(End))
                .When(_ => EventIsValidated(SecondEnd))
                .Then(_ => EventNotAccepted())
                .BDDfy();
        }
    }
}