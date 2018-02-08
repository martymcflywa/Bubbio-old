using Bubbio.Core.Events.Enums;

namespace Bubbio.Core.Events
{
    public interface ITransition : IEvent
    {
        Transition Transition { get; set; }
    }
}