namespace UI.Common;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        var viewModel = new SnackMachineViewModel();
        _dialogService.ShowDialog(viewModel);
    }
}
