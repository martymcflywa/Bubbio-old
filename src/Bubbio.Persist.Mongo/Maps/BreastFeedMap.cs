﻿using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Bubbio.Persist.Mongo.Maps
{
    [Serializable]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(BreastFeed))]
    public class BreastFeedMap : IMongoClassMap, ITransition
    {
        public long SequenceId { get; set; }

        [BsonId]
        public Guid EventId { get; set; }
        public Guid BabyId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventType EventType { get; set; }
        public Transition Transition { get; set; }
    }
}