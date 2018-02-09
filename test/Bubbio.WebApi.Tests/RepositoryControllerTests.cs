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
    }
}