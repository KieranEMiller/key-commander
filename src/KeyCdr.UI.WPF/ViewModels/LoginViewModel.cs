using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Users;
using KeyCdr.Data;
using KeyCdr.UI.WPF.Models;
using KeyCdr.UI.WPF.Util;

namespace KeyCdr.UI.WPF.ViewModels
{
    public class LoginViewModel 
    {
        public LoginViewModel()
        {
            _loginModel = new LoginModel();
            _userMgr = new Users.UserManager();
        }

        private LoginModel _loginModel;
        private UserManager _userMgr;

        public bool LoginSuccessful { get; set; }
        public KCUser User { get; set; }

        public LoginModel Model { get { return _loginModel; } }

        public event EventHandler<LoginViewClosedEventArgs> OnLoginClose;
        public void OnCloseEventHandler(LoginViewClosedEventArgs e)
        {
            EventHandler<LoginViewClosedEventArgs> handler = OnLoginClose;
            if (handler != null)
            {
                handler(this, e);
            }
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
                _loginModel.ErrorMsg = "invalid login name";
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
    }
}
