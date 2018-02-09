using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bubbio.Core
{
    public class EventJsonDeserializer : JsonConverter, IJsonDeserialize
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type type)
        {
            return type == typeof(IEvent);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }

        public override object ReadJson(JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var eventType = jObject[nameof(IEvent.EventType)].Value<EventType>();
            IEvent @event;

            switch (eventType)
            {
                case EventType.Sleep:
                    @event = new Sleep();
                    break;
                case EventType.BottleFeed:
                    @event = new BottleFeed();
                    break;
                case EventType.BreastFeed:
                    @event = new BreastFeed();
                    break;
                case EventType.TummyTime:
                    @event = new TummyTime();
                    break;
                case EventType.WeightUpdate:
                    @event = new WeightUpdate();
                    break;
                case EventType.HeightUpdate:
                    @event = new HeightUpdate();
                    break;
                default:
                    throw new InvalidOperationException($"EventType {(int) eventType} not supported.");
            }
            serializer.Populate(jObject.CreateReader(), @event);
            return @event;
        }
    }
}