namespace Domain;

public class SnackMachine : Entity
{
    public Money MoneyInside { get; private set; }
    public Money MoneyInTransaction { get; private set; }

    public void InsertMoney(Money money)
    {
        MoneyInTransaction += money;
    }

    public void ReturnMoney()
    {
        //MoneyInTransaction = 0;
    }

    private void BuySnack()
    {
        MoneyInside += MoneyInTransaction;
        //MoneyInTransaction = 0;
    }
}