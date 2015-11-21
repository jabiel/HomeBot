using HomeBot.Api.Models;
using System;

namespace HomeBot.Api.Services
{
    public class EventService
    {

        /// <summary>
        ///  saves events receiced from rpi and responds with messages to rpi
        /// </summary>
        /// <param name="model"></param>
        public EventMessageModel TransferMessage(EventMessageModel model)
        {
            Insert(model);
            return GetOutMessage();
        }

        /// <summary>
        /// Puts events send from rpi to api
        /// </summary>
        /// <param name="model"></param>
        private void Insert(EventMessageModel model)
        {

            
        }

        /// <summary>
        /// gets events wchich sholud be sent from api TO rpi
        /// </summary>
        /// <param name="model"></param>
        private EventMessageModel GetOutMessage()
        {
            // empty
            return new EventMessageModel() { SenderId = "api", SendDate = DateTime.UtcNow };
        }
    }
}