using System;
using Bubbio.Core.Events.Enums;

namespace Bubbio.Core.Events
{
    public class WeightUpdate : IMeasurement
    {
        public long SequenceId { get; set; }
        public Guid EventId { get; set; }
        public Guid BabyId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventType EventType { get; set; }
        public float Value { get; set; }
    }
}