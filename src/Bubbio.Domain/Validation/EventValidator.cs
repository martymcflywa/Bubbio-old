using System;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;

namespace Bubbio.Domain.Validation
{
    public sealed class EventValidator : IValidate
    {
        private readonly IRepository _repository;

        public EventValidator(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// To pass validation, incoming start and end events require a related opposite transition event to exist.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task<bool> IsValidAsync(IEvent @event)
        {
            return IsValid(@event)
                   && await IsValidAsync(@event as ITransition);
        }

        private static bool IsValid(IEvent @event)
        {
            return !@event.EventId.Equals(Guid.Empty)
                   && !@event.BabyId.Equals(Guid.Empty)
                   && @event.Timestamp > DateTimeOffset.MinValue;
        }

        private async Task<bool> IsValidAsync(ITransition @event)
        {
            if (@event == null)
                return true;

            // TODO: How about caching a set of last events in memory? Use resolver to lookup memory first.
            var lastEvent = (ITransition) await _repository.GetLastAsync(@event.BabyId, @event.EventType);

            if (@event.Transition.Equals(Transition.Start))
                return lastEvent == null || lastEvent.Transition.Equals(Transition.End);

            return lastEvent != null && lastEvent.Transition.Equals(Transition.Start);
        }
    }
}