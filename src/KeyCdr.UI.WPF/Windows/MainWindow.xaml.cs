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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KeyCdr.UI.WPF.ViewModels;

namespace KeyCdr.UI.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
            :this(null)
        { }

        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();
            _mainvm = vm;
            this.DataContext = _mainvm;
        }

        private MainViewModel _mainvm;
    }
}
