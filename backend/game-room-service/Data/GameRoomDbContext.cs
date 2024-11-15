
using Microsoft.EntityFrameworkCore;

namespace game_room_service.Models
{
    public class GameRoomDbContext : DbContext
    {
        public GameRoomDbContext(DbContextOptions<GameRoomDbContext> options)
            : base(options)
        {
        }

        public DbSet<GameRoom> GameRooms { get; set; }

        
    }
}