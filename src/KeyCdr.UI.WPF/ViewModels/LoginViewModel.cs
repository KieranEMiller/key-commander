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

        public string ErrorMsg { get; set; }
        public bool LoginSuccessful { get; set; }
        public KCUser User { get; set; }

        public event EventHandler<LoginViewClosedEventArgs> OnLoginClose;
        public void OnCloseEventHandler(LoginViewClosedEventArgs e)
        {
            EventHandler<LoginViewClosedEventArgs> handler = OnLoginClose;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public string LoginName {
            get { return _loginModel.LoginName; }
            set { _loginModel.LoginName = value; }
        }

        public void LoginAsGuest()
        {
            KCUser user = _userMgr.CreateGuest();
            ProcessLogin(user);
        }

        public void DoLogin()
        {
            KCUser user = _userMgr.GetByLoginName(_loginModel.LoginName);

            if(user == null) {
                ShowError("invalid login name");
            }
            else {
                ProcessLogin(user);
            }
        }

        public void ProcessLogin(KCUser user)
        {
            this.User = user;
            this.LoginSuccessful = (user != null);
            OnCloseEventHandler(new LoginViewClosedEventArgs() { User = user });
        }

        public void ShowError(string msg)
        {
            this.ErrorMsg = msg;
            RaisePropertyChanged("ErrorMsg");
        }
    }
}
