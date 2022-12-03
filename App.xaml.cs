using System.Diagnostics;
using System.Windows;

namespace Brainstormer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                Debug.WriteLine(e.Args[0] + e.Args[1]);
            }
            MainWindow login = new MainWindow(e.Args);
            login.Show();
        }
    }
}
