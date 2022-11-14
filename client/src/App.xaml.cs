using System.Windows;

namespace UI
{
    using Domain.Utils;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Initer.Init(@"Server=(local);Database=Ddd;Trusted_Connection=true");
        }
    }
}