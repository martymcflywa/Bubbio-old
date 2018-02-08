using System;
using Bubbio.Core.Events.Enums;

namespace Bubbio.Core.Events
{
    public class BottleFeedEvent : ITransitionEvent
    {
        public Guid EventId { get; set; }
        public Guid BabyId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventType EventType { get; set; }
        public Transition Transition { get; set; }
        public int Amount { get; set; }
    }
}