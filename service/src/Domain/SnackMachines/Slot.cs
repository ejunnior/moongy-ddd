namespace Domain.SnackMachines;

using Common;

public class Slot : Entity
{
    public Slot(
        SnackMachine snackMachine,
        int position) : this()
    {
        SnackMachine = snackMachine;
        Position = position;
        SnackPile = SnackPile.Empty;
    }

    private Slot()
    {
    }

    public int Position { get; }
    public SnackMachine SnackMachine { get; }
    public SnackPile SnackPile { get; set; }
}