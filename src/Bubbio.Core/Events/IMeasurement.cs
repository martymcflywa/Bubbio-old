using Newtonsoft.Json;

namespace Bubbio.Core.Events
{
    [JsonConverter(typeof(EventJsonDeserializer))]
    public interface IMeasurement : IEvent
    {
        float Value { get; set; }
    }
}