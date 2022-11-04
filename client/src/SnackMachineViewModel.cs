namespace UI;

using Common;
using Domain;

public class SnackMachineViewModel : ViewModel
{
    private readonly SnackMachine _snackMachine;

    public SnackMachineViewModel(SnackMachine snackMachine)
    {
        _snackMachine = snackMachine;
        InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
        InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
        InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
        InsertEuroCommand = new Command(() => InsertMoney(Money.Euro));
        InsertFiveEuroCommand = new Command(() => InsertMoney(Money.FiveEuro));
        InsertTwentyEuroCommand = new Command(() => InsertMoney(Money.TwentyEuro));
    }

    public override string Caption => "Snack Machine";
    public Command InsertCentCommand { get; private set; }
    public Command InsertEuroCommand { get; private set; }
    public Command InsertFiveEuroCommand { get; private set; }
    public Command InsertQuarterCommand { get; private set; }
    public Command InsertTenCentCommand { get; private set; }
    public Command InsertTwentyEuroCommand { get; private set; }

    public string MoneyInTransaction => _snackMachine.MoneyInTransaction.Amount.ToString();

    private void InsertMoney(Money money)
    {
        _snackMachine
            .InsertMoney(money);

        Notify("MoneyInTransaction");
    }
}