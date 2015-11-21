using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBot.Api.Models
{
    public class EventMessageModel
    {
        public IEnumerable<EventModel> Events { get; set; }
        public string SenderId { get; set; }
        public DateTime SendDate { get; set; }
    }
}