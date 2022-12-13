using System.Diagnostics;
using System.Windows;

namespace Brainstormer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Enables the use of launch parameters for quick login.
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Checks if the parameters exist
            if (e.Args.Length > 0)
            {
                Debug.WriteLine(e.Args[0] + e.Args[1]);
            }
            //Sends the parameters to the login window.
            MainWindow login = new(e.Args);
            login.Show();
        }
    }
}
