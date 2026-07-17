using System;

namespace ChatApi.Models{

public class Message
{
    public int Id {get; set;}
    public int ConversationId {get; set;}
    public string? Sender {get; set;}
    public string? Content { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    

}
}
