namespace Domain;

public class SnackMachine : Entity
{
    public Money MoneyInside { get; private set; } = Money.None;
    public Money MoneyInTransaction { get; private set; } = Money.None;

    public void BuySnack()
    {
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

    public void ReturnMoney()
    {
        MoneyInTransaction = Money.None;
    }
}