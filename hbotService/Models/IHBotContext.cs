using System.Data.Entity;
using hbotService.DataObjects;

namespace hbotService.Models
{
    public interface IHBotContext
    {
        DbSet<ReceivedEvent> ReceivedEvents { get; set; }
        DbSet<TodoItem> TodoItems { get; set; }
    }
}