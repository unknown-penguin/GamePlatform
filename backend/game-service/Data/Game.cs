namespace game_service.Data;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }

    public Game(int id, string name, string description, string image)
    {
        Id = id;
        Name = name;
        Description = description;
        Image = image;
    }

}
