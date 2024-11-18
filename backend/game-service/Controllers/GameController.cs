using game_service.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace game_service.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowLocalhost")]
public class GameController : ControllerBase
{
    private readonly GameDbContext _gameDbContext;
    private readonly ILogger<Game> _logger;

    public GameController(ILogger<Game> logger, GameDbContext gameDbContext)
    {
        _logger = logger;
        _gameDbContext = gameDbContext;
    }

    
    [HttpGet(Name = "GetGames")]
    public IEnumerable<Game> Get()
    {
        return _gameDbContext.Games.ToList();
    }

    [HttpGet("{id}",Name = "GetById")]
    public ActionResult<Game> GetById(int id)
    {
        var game = _gameDbContext.Games.Find(id);
        if (game == null)
        {
            return NotFound();
        }
        return game;
    }

    [HttpPost]
    
    public ActionResult<Game> Create(Game game)
    {
        _gameDbContext.Games.Add(game);
        _gameDbContext.SaveChanges();
        return CreatedAtRoute("GetGames", new { id = game.Id }, game);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Game game)
    {
        if (id != game.Id)
        {
            return BadRequest();
        }

        _gameDbContext.Entry(game).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        try
        {
            _gameDbContext.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_gameDbContext.Games.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var game = _gameDbContext.Games.Find(id);
        if (game == null)
        {
            return NotFound();
        }

        _gameDbContext.Games.Remove(game);
        _gameDbContext.SaveChanges();

        return NoContent();
    }
}
