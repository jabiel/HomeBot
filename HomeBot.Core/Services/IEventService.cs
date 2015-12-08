using HomeBot.Core.Model;
using System.Collections.Generic;

namespace HomeBot.Core.Services
{
    public interface IEventService
    {
        EventMessageModel TransferMessage(EventMessageModel model);
        IEnumerable<Event2Model> RecentEvents();
    }
}