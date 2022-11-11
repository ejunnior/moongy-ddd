namespace Domain;

public class Slot : Entity
{
    public Slot(
        SnackMachine snackMachine,
        int position) : this()
    {
        SnackMachine = snackMachine;
        Position = position;
        SnackPile = new SnackPile(null, 0, 0m);
    }

    private Slot()
    {
    }

    public int Position { get; }
    public SnackMachine SnackMachine { get; }
    public SnackPile SnackPile { get; set; }
}