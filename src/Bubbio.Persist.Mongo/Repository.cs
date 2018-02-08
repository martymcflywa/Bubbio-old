using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using MongoDB.Driver;

namespace Bubbio.Persist.Mongo
{
    public sealed class Repository : IRepository
    {
        private readonly IMongoCollection<IEvent> _collection;
        private readonly InsertManyOptions _options = new InsertManyOptions
        {
            IsOrdered = true
        };

        public Repository(string url, string schema)
        {
            var client = new MongoClient(url);
            var database = client.GetDatabase(schema);
            _collection = database.GetCollection<IEvent>(schema);
        }

        public async Task BatchInsertAsync(IEnumerable<IEvent> events)
        {
            await _collection
                .InsertManyAsync(events, _options);
        }

        public async Task InsertAsync(IEvent @event)
        {
            await _collection
                .InsertOneAsync(@event);
        }

        public async Task<IEnumerable<IEvent>> BatchGetAsync(Guid babyId)
        {
            return await _collection
                .Find(b => b.BabyId.Equals(babyId))
                .ToListAsync();
        }

        public async Task<IEvent> GetLastAsync(Guid babyId, EventType eventType)
        {
            return await _collection
                .Find(b => b.BabyId.Equals(babyId)
                           && b.EventType.Equals(eventType))
                .SortByDescending(e => e.Timestamp)
                .FirstAsync();
        }
    }
}