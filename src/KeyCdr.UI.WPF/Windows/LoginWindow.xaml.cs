using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KeyCdr.UI.WPF.ViewModels;

namespace KeyCdr.UI.WPF.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            _loginvm = new LoginViewModel();
            this.DataContext = _loginvm;
        }

        private LoginViewModel _loginvm;

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _loginvm.DoLogin();
        }
    }
}
