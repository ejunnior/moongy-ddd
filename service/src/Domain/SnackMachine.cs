namespace Domain;

public class SnackMachine : AggregateRoot
{
    public SnackMachine()
    {
        MoneyInside = Money.None;
        MoneyInTransaction = 0;

        Slots = new List<Slot>
        {
            new Slot(this,1),
            new Slot(this,2),
            new Slot(this,3)
        };
    }

    public Money MoneyInside { get; private set; }

    public decimal MoneyInTransaction { get; private set; }

    private IList<Slot> Slots { get; set; }

    public void BuySnack(int position)
    {
        var slot = GetSlot(position);

        if (slot.SnackPile.Price > MoneyInTransaction)
            throw new InvalidOperationException();

        slot.SnackPile = slot.SnackPile.SubtractOne();

        var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);

        if (change.Amount < MoneyInTransaction - slot.SnackPile.Price)
            throw new InvalidOperationException();

        MoneyInside -= change;
        MoneyInTransaction = 0;
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

        MoneyInTransaction += money.Amount;
        MoneyInside += money;
    }

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }

    public void LoadSnacks(int position, SnackPile snackPile)
    {
        var slot = GetSlot(position);
        slot.SnackPile = snackPile;
    }

    public void ReturnMoney()
    {
        var moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
        MoneyInside -= moneyToReturn;
        MoneyInTransaction = 0;
    }

    private Slot GetSlot(int position)
    {
        return Slots
            .Single(x => x.Position == position);
    }
}