namespace UI.Common;

using Domain;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        var viewModel = new SnackMachineViewModel(new SnackMachine());
        _dialogService.ShowDialog(viewModel);
    }
}