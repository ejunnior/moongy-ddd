namespace UI.Common;

using Atms;
using Domain;
using Domain.Atms;
using Domain.SnackMachines;
using SnackMachines;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        //SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
        //var viewModel = new SnackMachineViewModel(snackMachine);
        //_dialogService.ShowDialog(viewModel);

        var atm = new AtmRepository().GetById(1);
        var viewModel = new AtmViewModel(atm, new PaymentGateway());
        _dialogService.ShowDialog(viewModel);
    }
}