using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KeyCdr.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void RunApp(object sender, StartupEventArgs e)
        {
            //prevent the application from shutting down prematurely by
            //making shutdown explicit
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var loginvm = new ViewModels.LoginViewModel();
            var loginWindow = new Windows.LoginWindow(loginvm);
            loginvm.OnLoginClose += (s, args) =>
            {
                loginWindow.Close();    
            };
            loginWindow.ShowDialog();

            if (!loginvm.LoginSuccessful)
            {
                Application.Current.Shutdown();
            }
            else
            {
                ViewModels.MainViewModel mainvm = new ViewModels.MainViewModel(loginvm.User);
                Windows.MainWindow win = new Windows.MainWindow(mainvm);

                //now that we have a user we can switch back to standard window behavior
                //that will shutdown the application when the main window closes
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                App.Current.MainWindow = win;

                win.Show();
            }
        }
    }
}
