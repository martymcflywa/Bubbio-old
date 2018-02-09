using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;

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