
namespace game_room_service.Models;
public class GameRoom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxPlayers { get; set; }
    public bool IsPrivate { get; set; }
    public string Password { get; set; }


    public GameRoom(int id, string name, int maxPlayers, bool isPrivate, string password)
    {
        Id = id;
        Name = name;
        MaxPlayers = maxPlayers;
        IsPrivate = isPrivate;
        Password = password;
    }
}