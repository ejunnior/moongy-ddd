namespace UI.Common;

using Domain;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
        var viewModel = new SnackMachineViewModel(snackMachine);
        _dialogService.ShowDialog(viewModel);
    }
}