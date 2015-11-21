using HomeBot.Api.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeBot.Api.Controllers
{
    [AllowAnonymous]
    public class LightController : ApiController
    {
        // GET api/values
        public bool Get()
        {
            return StateContainer.Light;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(bool id)
        {
            StateContainer.Light = id;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
