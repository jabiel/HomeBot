using hbotService.DataObjects;
using HomeBot.Core.Interface;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBot.Pi.Providers.Api
{
    public class AzureMobileProvider
    {

        public async Task Send(IReceivedEvent evt)
        {
            var client = new MobileServiceClient(
                "https://hbot.azure-mobile.net/",
                "oaLnMgzwFYxiDHcKnqWFjjPfqtTIwe86"
            );

            var table = client.GetTable<ReceivedEvent>();

            var entity = new ReceivedEvent()
            {
                Name = evt.Name,
                Content = evt.Content,
                EventDate = evt.EventDate,
                SendDate = evt.SendDate,
                SenderId = evt.SenderId
            };

            await table.InsertAsync(entity);
        }
    }
}
