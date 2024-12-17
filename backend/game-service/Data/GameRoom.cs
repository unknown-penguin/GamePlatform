using System.ComponentModel.DataAnnotations.Schema;

namespace game_service.Data;
public class GameRoom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxPlayers { get; set; }
    public bool IsPrivate { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    [ForeignKey("Game")]
    public int GameId { get; set; }
    public Game Game { get; set; }

    public GameRoom(int id, string name, int maxPlayers, bool isPrivate, string password, string description, string image, int gameId)
    {
        Id = id;
        Name = name;
        MaxPlayers = maxPlayers;
        IsPrivate = isPrivate;
        Password = password;
        Description = description;
        Image = image;
        GameId = gameId;
    }
}