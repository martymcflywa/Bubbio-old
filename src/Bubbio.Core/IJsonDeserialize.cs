using System;
using Newtonsoft.Json;

namespace Bubbio.Core
{
    public interface IJsonDeserialize
    {
        bool CanConvert(Type type);
        void WriteJson(JsonWriter writer, object value, JsonSerializer serializer);
        object ReadJson(JsonReader reader, Type type, object value, JsonSerializer serializer);
    }
}