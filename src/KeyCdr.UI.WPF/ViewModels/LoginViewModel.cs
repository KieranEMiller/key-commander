using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Users;
using KeyCdr.Data;
using KeyCdr.UI.WPF.Models;

namespace KeyCdr.UI.WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public LoginViewModel()
        {
            _loginModel = new LoginModel();
            _userMgr = new Users.UserManager();
        }

        private LoginModel _loginModel;
        private UserManager _userMgr;

        public string LoginName {
            get { return _loginModel.LoginName; }
            set { _loginModel.LoginName = value; }
        }

        public string ErrorMsg { get; set; }

        public bool LoginSuccessful { get; set; }
        public KCUser User { get; set; }

        public void DoLogin()
        {
            KCUser user = _userMgr.GetByLoginName(_loginModel.LoginName);
            this.LoginSuccessful = (user != null);

            if(user == null) {
                ShowError("invalid login name");
            }
            else {
                this.User = user;

            }
        }

        public void ShowError(string msg)
        {
            this.ErrorMsg = msg;
            RaisePropertyChanged("ErrorMsg");
        }
    }
}
