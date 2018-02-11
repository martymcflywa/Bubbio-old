using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Bubbio.Persist.Mongo.Maps
{
    public class CreateBabyMap : IMongoClassMap
    {
        public long SequenceId { get; set; }
        public Guid EventId { get; set; }

        [BsonId]
        public Guid BabyId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventType EventType { get; set; }
        public Name Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}