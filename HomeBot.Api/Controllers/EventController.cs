using HomeBot.Core.Model;
using HomeBot.Core.Services;
using System.Web.Http;

namespace HomeBot.Api.Controllers
{
    [AllowAnonymous, RoutePrefix("api/event")]
    public class EventController : ApiController
    {
        private readonly IEventService _service;
        public EventController(IEventService service)
        {
            _service = service;
        }
        
        [HttpPost, Route("")]
        public IHttpActionResult TransferMessage(EventMessageModel model)
        {
            return Ok(_service.TransferMessage(model));
        }

        [HttpGet, Route("recent")]
        public IHttpActionResult RecentEvents()
        {
            return Ok(_service.RecentEvents());
        }


    }
}
