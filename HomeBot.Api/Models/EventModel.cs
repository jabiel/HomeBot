using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBot.Api.Models
{
    public class EventModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime EventDate { get; set; }
    }
}