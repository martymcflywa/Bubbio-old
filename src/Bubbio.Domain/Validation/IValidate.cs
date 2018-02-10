using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;

namespace Bubbio.Domain.Validation
{
    public interface IValidate
    {
        Task<IEvent> Validate(IRepository repository, IEvent @event);
    }
}