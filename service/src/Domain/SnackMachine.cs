namespace Domain;

public class SnackMachine : Entity
{
    public SnackMachine()
    {
        MoneyInside = Money.None;
        MoneyInTransaction = Money.None;

        Slots = new List<Slot>
        {
            new Slot(snackMachine: this,position:1,snack:null,quantity:0,price:0m),
            new Slot(snackMachine: this,position:2,snack:null,quantity:0,price:0m),
            new Slot(snackMachine: this,position:3,snack:null,quantity:0,price:0m)
        };
    }

    public Money MoneyInside { get; private set; }
    public Money MoneyInTransaction { get; private set; }

    public IList<Slot> Slots { get; set; }

    public void BuySnack(int position)
    {
        var slot = Slots.Single(x => x.Position == position);
        slot.Quantity -= 1;

        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = Money.None;
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

    public void LoadSnacks(
        int position,
        Snack snack,
        int quantity,
        decimal price)
    {
        var slot = Slots.Single(x => x.Position == position);
        slot.Snack = snack;
        slot.Quantity = quantity;
        slot.Price = price;
    }

    public void ReturnMoney()
    {
        MoneyInTransaction = Money.None;
    }
}