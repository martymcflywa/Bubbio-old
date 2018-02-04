using Bubbio.Core.Events.Enums;

namespace Bubbio.Core.Events
{
    public class BreastFeedEvent : FeedEvent
    {
        public Side Side { get; set; }
    }
}