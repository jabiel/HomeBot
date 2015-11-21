using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hbotService.Services
{
    public class EventService : IEventService
    {
        public string TestDI()
        {
            return "DI WORKS!";
        }
    }
}