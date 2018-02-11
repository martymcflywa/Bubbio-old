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
                   && IsValidBaby(@event as CreateBaby)
                   && await IsValidTransition(repository, @event as ITransition);
        }

        private static bool IsValidBaby(CreateBaby baby)
        {
            if (baby == null)
                return true;

            var first = baby.Name.First;
            var middle = baby.Name.Middle;
            var last = baby.Name.Last;

            baby.Name.First = first.Validate();
            baby.Name.Middle = middle.Validate();
            baby.Name.Last = last.Validate();

            return !baby.Name.First.IsEmpty() && !baby.Name.Last.IsEmpty();
        }

        private static async Task<bool> IsValidTransition(IRepository repository, ITransition @event)
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