using System.Threading.Tasks;
using Bubbio.Core.Events;

namespace Bubbio.Domain.Validation
{
    public interface IValidate
    {
        Task<bool> IsValidAsync(ITransition transitionEvent);
    }
}