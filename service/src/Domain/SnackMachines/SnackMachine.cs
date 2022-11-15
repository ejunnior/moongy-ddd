namespace Domain.SnackMachines;

using Common;
using SharedKernel;

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
        if (CanBuySnack(position) != String.Empty)
            throw new InvalidOperationException();

        var slot = GetSlot(position);

        slot.SnackPile = slot.SnackPile.SubtractOne();

        var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);

        MoneyInside -= change;
        MoneyInTransaction = 0;
    }

    public virtual string CanBuySnack(int position)
    {
        var snackPile = GetSnackPile(position);

        if (snackPile.Quantity == 0)
            return "The snack pile is empty";

        if (MoneyInTransaction < snackPile.Price)
            return "Not enough money";

        if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
            return "Not enough change";

        return string.Empty;
    }

    public IReadOnlyList<SnackPile> GetAllSnackPiles()
    {
        return Slots
            .OrderBy(x => x.Position)
            .Select(x => x.SnackPile)
            .ToList();
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

    public virtual Money UnloadMoney()
    {
        if (MoneyInTransaction > 0)
            throw new InvalidOperationException();

        Money money = MoneyInside;
        MoneyInside = Money.None;
        return money;
    }

    private Slot GetSlot(int position)
    {
        return Slots
            .Single(x => x.Position == position);
    }
}