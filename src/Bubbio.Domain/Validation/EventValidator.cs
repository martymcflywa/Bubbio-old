using System;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;
using Bubbio.Core.Exceptions;

namespace Bubbio.Domain.Validation
{
    public sealed class EventValidator : IValidate
    {
        /// <summary>
        /// To pass validation, incoming start and end events require a related opposite transition event to exist.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task<IEvent> Validate(IRepository repository, IEvent @event)
        {
            if (await IsValid(repository, @event))
                return @event;

            throw new InvalidEventException($"SequenceId={@event.SequenceId} EventType={@event.EventType} is invalid");
        }

        private static async Task<bool> IsValid(IRepository repository, IEvent @event)
        {
            return !@event.EventId.Equals(Guid.Empty)
                   && !@event.BabyId.Equals(Guid.Empty)
                   && @event.Timestamp > DateTimeOffset.MinValue
                   && await IsValidAsync(repository, @event as ITransition);
        }

        private static async Task<bool> IsValidAsync(IRepository repository, ITransition @event)
        {
            if (@event == null)
                return true;

            // TODO: How about caching a set of last events in memory? Use resolver to lookup memory first.
            var lastEvent = (ITransition) await repository.GetLastAsync(@event.BabyId, @event.EventType);

            if (@event.Transition.Equals(Transition.Start))
                return lastEvent == null || lastEvent.Transition.Equals(Transition.End);

            return lastEvent != null && lastEvent.Transition.Equals(Transition.Start);
        }
    }
}