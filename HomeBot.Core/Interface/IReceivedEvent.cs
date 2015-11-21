using System;

namespace HomeBot.Core.Interface
{
    public interface IReceivedEvent
    {
        string Content { get; set; }
        DateTime EventDate { get; set; }
        string Name { get; set; }
        DateTime SendDate { get; set; }
        string SenderId { get; set; }
    }
}