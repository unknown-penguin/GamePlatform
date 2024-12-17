using game_service.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GameServiceDbContext");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://gameplatfromweb-bxfthmeccbbcavb5.northeurope-01.azurewebsites.net",
                            "http://localhost:4200")
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
    if (builder.Environment.IsProduction())
        options.ListenAnyIP(7274, listenOptions => listenOptions.UseHttps("/app/certificate.pfx", "365281")); // HTTPS
    if (builder.Environment.IsDevelopment())
        options.ListenLocalhost(7274, listenOptions => listenOptions.UseHttps("E:\\project\\GamePlatform\\certificate.pfx", "365281")); // HTTPS
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();
app.Run();
