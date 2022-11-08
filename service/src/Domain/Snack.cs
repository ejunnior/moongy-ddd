namespace Domain;

public class Snack : Entity
{
    public Snack(string name)
    {
        Name = name;
    }

    private Snack()
    {
    }

    public string Name { get; }
}