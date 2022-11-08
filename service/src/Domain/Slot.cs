namespace Domain;

public class Slot : Entity
{
    public Slot(
        SnackMachine snackMachine,
        int position,
        Snack snack,
        int quantity,
        decimal price)
    {
        Position = position;
        Price = price;
        Quantity = quantity;
        Snack = snack;
        SnackMachine = snackMachine;
    }

    private Slot()
    {
    }

    public int Position { get; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Snack Snack { get; set; }
    public SnackMachine SnackMachine { get; }
}