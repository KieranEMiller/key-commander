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
            Window login = new Windows.LoginWindow();
            login.ShowDialog();

            var loginvm = login.DataContext as ViewModels.LoginViewModel;
            if (!loginvm.LoginSuccessful)
            {
                System.Windows.Application.Current.Shutdown();
            }

            Windows.MainWindow win = new Windows.MainWindow();
            win.Show();
        }
    }
}
