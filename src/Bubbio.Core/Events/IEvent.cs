using System;
using Bubbio.Core.Events.Enums;
using Newtonsoft.Json;

namespace Bubbio.Core.Events
{
    [JsonConverter(typeof(EventJsonDeserializer))]
    public interface IEvent
    {
        long SequenceId { get; set; }
        Guid EventId { get; set; }
        Guid BabyId { get; set; }
        DateTimeOffset Timestamp { get; set; }
        EventType EventType { get; set; }
    }
}