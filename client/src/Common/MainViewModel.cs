namespace UI.Common;

using Management;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        Dashboard = new DashboardViewModel();
    }

    public DashboardViewModel Dashboard { get; private set; }
}