using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.DTOs.Message
{
    public class CreateMessageRequest
    {
        public string Sender { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}