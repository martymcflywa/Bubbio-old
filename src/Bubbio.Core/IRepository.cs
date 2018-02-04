using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bubbio.Core.Events;

namespace Bubbio.Core
{
    public interface IRepository
    {
        Task<IEnumerable<IEvent>> GetEventsAsync(Guid babyId);
        Task<IEvent> GetLastEventAsync(Guid babyId, EventType eventType);
    }
}