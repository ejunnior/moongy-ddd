namespace Domain.Atms;

using Common;
using Microsoft.SqlServer.Server;
using SharedKernel;

public class Atm : AggregateRoot
{
    private const decimal ComissionRate = 0.01m;

    public decimal MoneyCharged { get; private set; }

    public Money MoneyInside { get; private set; } = Money.None;

    public decimal CaluculateAmountWithCommission(decimal amount)
    {
        var comission = amount * ComissionRate;
        var lessThanCent = comission % 0.01m;

        if (lessThanCent > 0)
            comission = comission - lessThanCent + 0.01m;

        return amount + comission;
    }

    public string CanTakeMoney(decimal amount)
    {
        if (amount <= 0)
            return "Invalid amount";

        if (MoneyInside.Amount < amount)
            return "Invalid amount";

        if (!MoneyInside.CanAllocate(amount))
            return "Invalid amount";

        return string.Empty;
    }

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }

    public void TakeMoney(decimal amount)
    {
        if (CanTakeMoney(amount) != string.Empty)
            throw new InvalidOperationException();

        var output = MoneyInside.Allocate(amount);
        MoneyInside -= output;

        var amountWithComission = CaluculateAmountWithCommission(amount);
        MoneyCharged += amountWithComission;
    }
}