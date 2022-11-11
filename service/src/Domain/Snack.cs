namespace Domain;

public class Snack : AggregateRoot
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