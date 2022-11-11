namespace Domain;

public class SnackMachine : AggregateRoot
{
    public SnackMachine()
    {
        MoneyInside = Money.None;
        MoneyInTransaction = Money.None;

        Slots = new List<Slot>
        {
            new Slot(this,1),
            new Slot(this,2),
            new Slot(this,3)
        };
    }

    public Money MoneyInside { get; private set; }

    public Money MoneyInTransaction { get; private set; }

    private IList<Slot> Slots { get; set; }

    public void BuySnack(int position)
    {
        var slot = GetSlot(position);
        slot.SnackPile = slot.SnackPile.SubtractOne();

        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = Money.None;
    }

    public SnackPile GetSnackPile(int position)
    {
        return GetSlot(position).SnackPile;
    }

    public void InsertMoney(Money money)
    {
        Money[] coinsAndNotes =
        {
            Money.Cent,
            Money.TenCent,
            Money.Quarter,
            Money.Euro,
            Money.FiveEuro,
            Money.TwentyEuro
        };

        if (!coinsAndNotes.Contains(money))
            throw new InvalidOperationException();

        MoneyInTransaction += money;
    }

    public void LoadSnacks(int position, SnackPile snackPile)
    {
        var slot = GetSlot(position);
        slot.SnackPile = snackPile;
    }

    public void ReturnMoney()
    {
        MoneyInTransaction = Money.None;
    }

    private Slot GetSlot(int position)
    {
        return Slots
            .Single(x => x.Position == position);
    }
}