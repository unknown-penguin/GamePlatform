using game_service.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GameServiceDbContext");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://gameplatfromweb-bxfthmeccbbcavb5.northeurope-01.azurewebsites.net")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddDbContext<GameDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("GameServiceDbContext"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Kestrel to listen on both HTTP and HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5031); // HTTP
    options.ListenAnyIP(7274, listenOptions => listenOptions.UseHttps("/app/certificate.pfx", "365281")); // HTTPS
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

app.UseRouting();
// app.Use(async (context, next) =>
// {
//     context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
//     await next.Invoke();
// });

app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();
app.Run();
