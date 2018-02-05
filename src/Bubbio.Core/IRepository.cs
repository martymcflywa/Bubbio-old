using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bubbio.Core.Events;

namespace Bubbio.Core
{
    public interface IRepository
    {
        Task BatchInsertAsync(IEnumerable<IEvent> events);
        Task InsertAsync(IEvent @event);
        Task<IEnumerable<IEvent>> BatchGetAsync(Guid babyId);
        Task<IEvent> GetLastAsync(Guid babyId, EventType eventType);
    }
}