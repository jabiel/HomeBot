using HomeBot.Api.Models;
using HomeBot.Api.Services;
using System.Web.Http;

namespace HomeBot.Api.Controllers
{
    [AllowAnonymous, RoutePrefix("api/event")]
    public class EventController : ApiController
    {
        private readonly EventService _service = new EventService();
        public EventController()
        {

        }
        
        [HttpPost, Route("")]
        public IHttpActionResult TransferMessage(EventMessageModel model)
        {
            return Ok(_service.TransferMessage(model));
        }

        [HttpGet, Route("recentEvents")]
        public IHttpActionResult RecentEvents(EventMessageModel model)
        {
            return Ok(_service.TransferMessage(model));
        }


    }
}
