using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBot.Tests.Fakes;
using HomeBot.Core.Services;
using HomeBot.Core.Model.Context;
using System;
using HomeBot.Core.Model;
using System.Collections.Generic;

namespace HomeBot.Tests
{
    [TestClass]
    public class EventServiceTests
    {
        [TestMethod]
        public void ReadAlarmMessage()
        {
            var db = new HomeBotEntities();
            var service = new EventService(db);

            var msg = new EventMessageModel() {
                SenderId = "34567",
                SendDate = DateTime.Now,
                Events = new List<EventModel>() {
                    new EventModel() { EventDate = DateTime.Now, Name="test", Value="aqq" }
                }
            };

            var result = service.TransferMessage(msg);

            Assert.IsNotNull(result);

        }
        
    }
}
