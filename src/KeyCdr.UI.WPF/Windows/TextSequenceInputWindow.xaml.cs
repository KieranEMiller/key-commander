using KeyCdr.UI.WPF.ViewModels;
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

namespace KeyCdr.UI.WPF.Windows
{
    /// <summary>
    /// Interaction logic for TextSequenceInputWindow.xaml
    /// </summary>
    public partial class TextSequenceInputWindow : Window
    {
        public TextSequenceInputWindow()
            :this(null)
        { }

        public TextSequenceInputWindow(TextSequenceInputViewModel vm)
        {
            InitializeComponent();

            _inputvm = vm;
            this.DataContext = _inputvm;

            txtInput.Focus();
        }

        private TextSequenceInputViewModel _inputvm;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            _inputvm.RegisterKeyDown(e);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!txtInput.IsFocused)
                txtInput.Focus();
        }
    }
}
