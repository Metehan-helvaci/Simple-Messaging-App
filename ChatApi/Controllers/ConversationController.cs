using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.DTOs.Conversation;
using ChatApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private ChatDbContext _context;
        public ConversationController(ChatDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateConversationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Sohbet başlığı boş olamaz.");
            }
            Conversation conversation = new Conversation
            {
                Title = request.Title
            };

            _context.Conversations.Add(conversation);
            _context.SaveChanges();

            return Ok(new ConversationList
            {
                Id = conversation.Id,
                Title = conversation.Title,
                CreatedAt = conversation.CreatedAt
            });

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            
            List<ConversationList> conversations = _context.Conversations.Select(c => new ConversationList
            {
                Id = c.Id,
                Title = c.Title,
                CreatedAt = c.CreatedAt
            }).ToList();

            return Ok(conversations);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _context.Conversations.FirstOrDefault(c => c.Id == id);  
            if (result == null)
            {
                return NotFound("Sohbet Bulunamadı");
            }
            ConversationDetailResponse response = new()
            {
                Id = result.Id,
                Title = result.Title,
                CreatedAt = result.CreatedAt,
                MessageCount = _context.Messages.Count(m => m.ConversationId == id),
                Participants = _context.Messages
                .Where(m => m.ConversationId == id)
                .Select(m => m.Sender!)
                .Distinct()
                .ToList()
            };
            return Ok(response);
        }
    }
}