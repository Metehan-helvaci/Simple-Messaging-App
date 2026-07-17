using System;


namespace ChatApi.Models{

    public class Conversation
    {
    public int Id {get;set;}
    public string? Title{get; set;}
    public DateTime CreatedAt{get; set;} = DateTime.UtcNow;
    
    }
}
