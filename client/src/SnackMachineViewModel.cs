namespace UI;

using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Domain;

public class SnackMachineViewModel : ViewModel
{
    private readonly SnackMachineRepository _repository;
    private readonly SnackMachine _snackMachine;

    private string _menssage = string.Empty;

    public SnackMachineViewModel(SnackMachine snackMachine)
    {
        _snackMachine = snackMachine;
        _repository = new SnackMachineRepository();
        InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
        InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
        InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
        InsertEuroCommand = new Command(() => InsertMoney(Money.Euro));
        InsertFiveEuroCommand = new Command(() => InsertMoney(Money.FiveEuro));
        InsertTwentyEuroCommand = new Command(() => InsertMoney(Money.TwentyEuro));
        ReturnMoneyCommand = new Command(() => ReturnMoney());
        BuySnackCommand = new Command<string>(BuySnack);
    }

    public Command<string> BuySnackCommand { get; private set; }

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

    public Money MoneyInside => _snackMachine.MoneyInside;

    public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();

    public IReadOnlyCollection<SnackPileViewModel> Piles
    {
        get
        {
            return _snackMachine.GetAllSnackPiles()
                .Select(x => new SnackPileViewModel(x))
                .ToList();
        }
    }

    public Command ReturnMoneyCommand { get; private set; }

    public void BuySnack(string position)
    {
        var pos = int.Parse(position);

        var error = _snackMachine.CanBuySnack(pos);

        if (error != String.Empty)
        {
            NotifyClient(error);
            return;
        }

        _snackMachine.BuySnack(pos);
        _repository.Save(_snackMachine);
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
        Notify(nameof(Piles));
    }
}