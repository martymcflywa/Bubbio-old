using System;

namespace Bubbio.Core.Events
{
    public class HeightUpdateEvent : IEvent
    {
        public Guid EventId { get; set; }
        public Guid BabyId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventType EventType { get; set; }
        public float Height { get; set; }
    }
}