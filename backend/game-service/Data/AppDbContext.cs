using Microsoft.EntityFrameworkCore;

namespace game_service.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameRoom> GameRooms { get; set; }
    }

}
