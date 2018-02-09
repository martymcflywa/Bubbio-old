using System;
using System.Net;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Core.Events;
using Bubbio.Core.Exceptions;
using Bubbio.Domain.Validation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using SerilogTimings;

namespace Bubbio.WebApi.Controllers
{
    public class RepositoryController : Controller
    {
        private readonly IRepository _repository;
        private readonly IValidate _validator;

        private const string ContentType = "application/json";

        public RepositoryController(IRepository repository, IValidate validator)
        {
            _repository = repository;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> InsertEventAsync([FromBody] string json)
        {
            var @event = JsonConvert.DeserializeObject<IEvent>(json);
            var result = new JsonResult(default) {ContentType = ContentType};

            using (var op = Operation.Begin("Inserting event {sequenceId} {eventType}", @event.SequenceId,
                @event.SequenceId))
            {
                try
                {
                    var eventType = @event.EventType;
                    if (await _validator.IsValidAsync(@event))
                        await _repository.InsertAsync(@event);
//                    else
//                        throw new TransitionEventException(eventType, asTransitionEvent.Transition);

                    result.Value = @event;
                    result.StatusCode = (int) HttpStatusCode.OK;
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error inserting event");
                    throw;
                }
                op.Complete();
            }

            return result;
        }
    }
}