namespace UI;

using Common;
using Domain;

public class SnackMachineViewModel : ViewModel
{
    private readonly SnackMachine _snackMachine;

    private string _menssage = string.Empty;

    public SnackMachineViewModel(SnackMachine snackMachine)
    {
        _snackMachine = snackMachine;
        InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
        InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
        InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
        InsertEuroCommand = new Command(() => InsertMoney(Money.Euro));
        InsertFiveEuroCommand = new Command(() => InsertMoney(Money.FiveEuro));
        InsertTwentyEuroCommand = new Command(() => InsertMoney(Money.TwentyEuro));
        ReturnMoneyCommand = new Command(() => ReturnMoney());
        BuySnackCommand = new Command(() => BuySnack());
    }

    public Command BuySnackCommand { get; private set; }
    public override string Caption => "Snack Machine";
    public Command InsertCentCommand { get; private set; }

    public Command InsertEuroCommand { get; private set; }

    public Command InsertFiveEuroCommand { get; private set; }

    public Command InsertQuarterCommand { get; private set; }

    public Command InsertTenCentCommand { get; private set; }

    public Command InsertTwentyEuroCommand { get; private set; }

    public string Message
    {
        get
        {
            return _menssage;
        }
        set
        {
            _menssage = value;
            Notify();
        }
    }

    public Money MoneyInside => _snackMachine.MoneyInside + _snackMachine.MoneyInTransaction;
    public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
    public Command ReturnMoneyCommand { get; private set; }

    public void BuySnack()
    {
        _snackMachine.BuySnack(1);
        using (var session = SessionFactory.OpenSession())
        using (var transaction = session.BeginTransaction())
        {
            session.SaveOrUpdate(_snackMachine);
            transaction.Commit();
        }
        NotifyClient("You have bought a snack");
    }

    public void ReturnMoney()
    {
        _snackMachine.ReturnMoney();
        NotifyClient("Money was returned");
    }

    private void InsertMoney(Money coinOrNote)
    {
        _snackMachine
            .InsertMoney(coinOrNote);

        NotifyClient("You have inserted " + coinOrNote);
    }

    private void NotifyClient(string message)
    {
        Message = message;
        Notify(nameof(MoneyInTransaction));
        Notify(nameof(MoneyInside));
    }
}