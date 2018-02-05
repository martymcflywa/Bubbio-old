using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;

namespace Bubbio.Domain.Validation
{
    internal sealed class TransitionValidator
    {
        private readonly IRepository _repository;

        public TransitionValidator(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// To pass validation, incoming start and end events require a related opposite transition event to exist.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task<bool> IsValidAsync(ITransitionEvent @event)
        {
            // TODO: How about caching a set of last events in memory? Use resolver to lookup memory first.
            var lastEvent = (ITransitionEvent) await _repository.GetLastAsync(@event.BabyId, @event.EventType);

            if (@event.Transition.Equals(Transition.Start))
                return lastEvent == null || lastEvent.Transition.Equals(Transition.End);

            return lastEvent != null && lastEvent.Transition.Equals(Transition.Start);
        }
    }
}