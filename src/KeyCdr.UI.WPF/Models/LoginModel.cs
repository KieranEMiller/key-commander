using KeyCdr.UI.WPF.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.UI.WPF.Models
{
    public class LoginModel : BasePropertyChanged, INotifyPropertyChanged
    {
        public LoginModel()
        { }

        private string _loginName;
        private string _errorMsg;

        public string LoginName {
            get { return _loginName; }
            set {
                _loginName = value;
                RaisePropertyChanged(nameof(this.LoginName));
            }
        }

        public string ErrorMsg {
            get { return _errorMsg; }
            set {
                _errorMsg = value;
                RaisePropertyChanged(nameof(this.ErrorMsg));
            }
        }
    }
}
