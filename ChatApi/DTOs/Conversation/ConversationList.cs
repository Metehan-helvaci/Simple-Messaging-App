using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.DTOs.Conversation
{
    public class ConversationList
    {
        public int Id {get; set;}
        public string? Title {get; set;}
        public DateTime CreatedAt { get; set; }
    }
}