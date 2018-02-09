using Bubbio.Core.Events.Enums;
using Newtonsoft.Json;

namespace Bubbio.Core.Events
{
    [JsonConverter(typeof(EventJsonDeserializer))]
    public interface ITransition : IEvent
    {
        Transition Transition { get; set; }
    }
}