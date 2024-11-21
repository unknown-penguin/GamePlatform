using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using game_room_service.Models;
using Microsoft.AspNetCore.Cors;
namespace game_room_service.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class GameRoomController : ControllerBase
{
    private readonly GameRoomDbContext _context;
    public GameRoomController(GameRoomDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameRoom>>> GetGameRooms()
    {
        return await _context.GameRooms.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<GameRoom>> GetGameRoom(int id)
    {
        var gameRoom = await _context.GameRooms.FindAsync(id);
        if (gameRoom == null)
        {
            return NotFound();
        }
        return gameRoom;
    }
    [HttpGet("gamerooms/{gameId}")]
    public async Task<ActionResult<IEnumerable<GameRoom>>> GetGameRoomByGameId(int gameId)
    {
        Console.WriteLine(gameId);
        var gameRoom = await _context.GameRooms.Where(g => g.GameId == gameId).ToListAsync();
        if (gameRoom == null)
        {
            return NotFound();
        }
        return gameRoom;
    }
    [HttpPost]
    public async Task<ActionResult<GameRoom>> PostGameRoom(GameRoom gameRoom)
    {
        _context.GameRooms.Add(gameRoom);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetGameRoom", new { id = gameRoom.Id }, gameRoom);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameRoom(int id)
    {
        var gameRoom = await _context.GameRooms.FindAsync(id);
        if (gameRoom == null)
        {
            return NotFound();
        }
        _context.GameRooms.Remove(gameRoom);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    private bool GameRoomExists(int id)
    {
        return _context.GameRooms.Any(e => e.Id == id);
    }
}
