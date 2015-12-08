using System;
using System.Collections.Generic;

namespace HomeBot.Core.Model
{
    public class EventMessageModel
    {
        public IEnumerable<EventModel> Events { get; set; }
        public string SenderId { get; set; }
        public DateTime SendDate { get; set; }
    }
}