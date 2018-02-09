using Bubbio.Domain.Tests.Scenarios;
using TestStack.BDDfy;
using Xunit;

namespace Bubbio.Domain.Tests
{
    public class TransitionValidatorTests : TransitionValidatorTestBase
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
    }
}