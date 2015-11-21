using hbotService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hbotService.Controllers
{
    [RoutePrefix("debug"), AllowAnonymous]
    public class DebugController : ApiController
    {
        private readonly IEventService _service;
        public DebugController(IEventService service)
        {
            _service = service;
        }
        
        [HttpGet, Route("test")]
        public IHttpActionResult TestDI()
        {
            return Ok(_service.TestDI());
        }

        [HttpGet, Route("exception")]
        public IHttpActionResult Exception()
        {
            throw new DivideByZeroException("Fuck yeah");
            return Ok();
        }
    }
}
