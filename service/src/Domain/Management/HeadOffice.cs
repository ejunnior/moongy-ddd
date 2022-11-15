namespace Domain.Management;

using Common;
using SharedKernel;
using Atms;
using SnackMachines;

public class HeadOffice : AggregateRoot
{
    public decimal Balance { get; private set; }

    public Money Cash { get; private set; } = Money.None;

    public void ChangeBalance(decimal delta)
    {
        Balance += delta;
    }

    public virtual void LoadCashToAtm(Atm atm)
    {
        atm.LoadMoney(Cash);
        Cash = Money.None;
    }

    public virtual void UnloadCashFromSnackMachine(SnackMachine snackMachine)
    {
        Money money = snackMachine.UnloadMoney();
        Cash += money;
    }
}