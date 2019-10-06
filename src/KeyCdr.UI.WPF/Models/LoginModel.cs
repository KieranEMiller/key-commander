using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.UI.WPF.Models
{
    public class LoginModel
    {
        public LoginModel()
        { }
        
        public string LoginName { get; set; }
        public string ErrorMsg { get; set; }

        /*
        private string _loginName;
        public string LoginName {
            get { return _loginName; }
            set {
                _loginName = value;
            }
        }

        private string _errMsg;
        public string ErrorMsg {
            get { return _errMsg; }
            set {
                _errMsg = value;
            }
        }*/
    }
}
