using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Persist.Mongo.Maps;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Bubbio.Persist.Mongo
{
    public sealed class Repository : IRepository, IRegisterMap
    {
        private readonly IMongoCollection<IMongoClassMap> _collection;
        private readonly InsertManyOptions _insertManyOptions;

        public Repository(string connectionString, string schema, string collection)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(schema);

            _collection = database.GetCollection<IMongoClassMap>(collection);
            _insertManyOptions = new InsertManyOptions { IsOrdered = true };

            Register();
        }

        private void Register()
        {
            Register<SleepMap>();
            Register<BottleFeedMap>();
            Register<TummyTimeMap>();
            Register<WeightUpdateMap>();
            Register<HeightUpdateMap>();
        }

        public void Register<TClassMap>() where TClassMap : IMongoClassMap
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(TClassMap)))
                BsonClassMap.RegisterClassMap<TClassMap>();
        }

        public async Task BatchInsertAsync(IEnumerable<IEvent> events)
        {
            await _collection.InsertManyAsync(events.ToClassMap(), _insertManyOptions);
        }

        public async Task InsertAsync(IEvent @event)
        {
            await _collection.InsertOneAsync(@event.ToClassMap());
        }

        public async Task<IEnumerable<IEvent>> BatchGetAsync(Guid babyId)
        {
            return await _collection.Find(b => b.BabyId.Equals(babyId))
                .ToListAsync();
        }

        public async Task<IEvent> GetLastAsync(Guid babyId, EventType eventType)
        {
            return await _collection.Find(b => b.BabyId.Equals(babyId)
                           && b.EventType.Equals(eventType))
                .SortByDescending(e => e.Timestamp)
                .FirstAsync();
        }
    }
}