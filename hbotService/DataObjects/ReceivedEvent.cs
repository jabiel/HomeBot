using HomeBot.Core.Interface;
using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.ComponentModel.DataAnnotations;

namespace hbotService.DataObjects
{
    public class ReceivedEvent : EntityData, IReceivedEvent
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public DateTime SendDate { get; set; }
    }
}