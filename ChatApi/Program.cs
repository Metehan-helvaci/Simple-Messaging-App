using ChatApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ChatDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();



//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();