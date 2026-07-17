using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.DTOs.Message
{
    public class MessageResponse
    {
        public int Id { get; set; }

        public string? Sender { get; set; } 

        public string? Content { get; set; } 

        public DateTime SentAt { get; set; }
    }
}