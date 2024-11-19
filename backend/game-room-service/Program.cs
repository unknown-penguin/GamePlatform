using Microsoft.EntityFrameworkCore;
using game_room_service.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameServiceDbContext");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.Services.AddDbContext<GameRoomDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("GameServiceDbContext"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Kestrel to listen on both HTTP and HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5049); // HTTP
    options.ListenAnyIP(7168, listenOptions => listenOptions.UseHttps("/app/certificate.pfx", "365281")); // HTTPS
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP to HTTPS in production environment if necessary
if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowLocalhost");
app.UseAuthorization();
app.MapControllers();
app.Run();
