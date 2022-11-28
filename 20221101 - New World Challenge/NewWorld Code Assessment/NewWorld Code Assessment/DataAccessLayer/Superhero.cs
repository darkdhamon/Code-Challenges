namespace NewWorld_Code_Assessment.DataAccessLayer;

public class Superhero
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Ability> Abilities { get; set; } = new();
}

public class Ability
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}