using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.DTOs.Message;
using ChatApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("api/conversation/{conversationId}/messages")]
    public class MessageController : ControllerBase
    {
        private ChatDbContext _context;

        public MessageController(ChatDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Create(int conversationId, CreateMessageRequest request)
        {
            Conversation? conversation = _context.Conversations.FirstOrDefault(c =>c.Id == conversationId);
            if (conversation == null)
            {
               return NotFound("Sohbet bulunamadı.");
            }
            if (string.IsNullOrWhiteSpace(request.Sender)|| string.IsNullOrWhiteSpace(request.Content))
            {
                return BadRequest("Gönderen ve mesaj boş olamaz.");
            }
            Message message = new Message()
            {
                ConversationId = conversationId,
                Sender = request.Sender,
                Content = request.Content,
            };
            _context.Messages.Add(message);
            _context.SaveChanges();

            return Ok(new MessageResponse()
            {
                Id = message.Id,
                SentAt = message.SentAt,
                Sender = message.Sender,
                Content = message.Content,
            });

        }      
        [HttpGet]
        public IActionResult GetAll(int conversationId)
        {
            Conversation? conversation = _context.Conversations.FirstOrDefault(c => c.Id == conversationId);
            if (conversation == null)
            {
                return NotFound("Sohbet bulunamadı.");
            }
            List<MessageResponse>messages = _context.Messages.Where(m =>m.ConversationId == conversationId)
            .OrderBy(m => m.SentAt)
            .Select(m => new MessageResponse()
            {
                Id = m.Id,
                Sender = m.Sender,
                Content = m.Content,
                SentAt = m.SentAt
            }).ToList();
            return Ok(messages);
        }
    }
}