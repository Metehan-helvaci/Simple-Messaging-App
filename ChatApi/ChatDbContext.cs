using ChatApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApi
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {

        }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}