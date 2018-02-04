using Bubbio.Core.Events.Enums;

namespace Bubbio.Core.Events
{
    public interface ITransitionEvent : IEvent
    {
        Transition Transition { get; set; }
    }
}