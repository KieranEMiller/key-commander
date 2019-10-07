using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.UI.WPF.ViewModels
{
    public class LoginViewClosedEventArgs : EventArgs
    {
        public KeyCdr.Data.KCUser User { get; set; }
    }
}
