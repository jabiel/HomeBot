using HomeBot.Core.Model;
using HomeBot.Core.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBot.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IHomeBotContext _ctx;
        public EventService(IHomeBotContext ctx)
        {
            _ctx = ctx;
        }


        /// <summary>
        ///  saves events receiced from rpi and responds with messages to rpi
        /// </summary>
        /// <param name="model"></param>
        public EventMessageModel TransferMessage(EventMessageModel model)
        {
            Insert(model);
            ProcessInserted(model);
            return GetOutMessage();
        }

        /// <summary>
        /// Puts events send from rpi to api
        /// </summary>
        /// <param name="model"></param>
        private void Insert(EventMessageModel model)
        {
            var events = model.Events.Select(p => new ReceivedEvent()
            {
                Name = p.Name,
                Content = p.Value,
                CreateDate = DateTime.UtcNow,
                EventDate = p.EventDate,
                SendDate = model.SendDate,
                SenderId = model.SenderId
            });
            _ctx.ReceivedEvents.AddRange(events);
            _ctx.SaveChanges();

        }

        private void ProcessInserted(EventMessageModel model)
        {

        }

        /// <summary>
        /// gets events wchich sholud be sent from api TO rpi
        /// </summary>
        /// <param name="model"></param>
        private EventMessageModel GetOutMessage()
        {
            //_ctx.SentEvents.Where(p=>p.SendDate);
            // empty
            return new EventMessageModel() { SenderId = "api", SendDate = DateTime.UtcNow };
        }

        public IEnumerable<Event2Model> RecentEvents()
        {
            var lastDay = DateTime.UtcNow.AddDays(-1);
            return _ctx.ReceivedEvents.Where(p => p.CreateDate > lastDay)
                .Select(p => new Event2Model() {
                    Name = p.Name,
                    Content = p.Content,
                    //CreateDate = DateTime.UtcNow,
                    EventDate = p.EventDate,
                    SendDate = p.SendDate,
                    SenderId = p.SenderId
                });
        }
    }
}