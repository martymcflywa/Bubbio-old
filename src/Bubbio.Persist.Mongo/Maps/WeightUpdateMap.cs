using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Bubbio.Persist.Mongo.Maps
{
    [Serializable]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(WeightUpdate))]
    public class WeightUpdateMap : IMongoClassMap, IMeasurement
    {
        public Guid EventId { get; set; }
        public Guid BabyId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventType EventType { get; set; }
        public float Value { get; set; }
    }
}