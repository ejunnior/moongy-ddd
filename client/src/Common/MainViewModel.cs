namespace UI.Common;

using Domain;
using Domain.SnackMachines;
using SnackMachines;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
        var viewModel = new SnackMachineViewModel(snackMachine);
        _dialogService.ShowDialog(viewModel);
    }
}