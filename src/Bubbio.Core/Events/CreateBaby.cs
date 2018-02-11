using System;
using Bubbio.Core.Events.Enums;

namespace Bubbio.Core.Events
{
    public class CreateBaby : IEvent
    {
        public long SequenceId { get; set; }
        public Guid EventId { get; set; }
        public Guid BabyId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventType EventType { get; set; }
        public Name Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}