using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.UI.WPF.Models;
using KeyCdr.Data;
using System.ComponentModel;

namespace KeyCdr.UI.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public MainViewModel()
            : this(new KCUser())
        {}

        public MainViewModel(KCUser user)
        {
            _mainModel = new MainModel();
            _mainModel.User = user;
            _mainModel.RecentSessions = new KeyCdr.History.UserSessionHistory().GetSessionsByUser(user);
        }

        private MainModel _mainModel;

        public KCUser User {
            get {
                return _mainModel.User;
            }
            set {
                _mainModel.User = value;
                RaisePropertyChanged("WelcomeMsg");
            }
        }

        public string WelcomeMsg {
            get {
                if(_mainModel.User != null)
                    return string.Format("Welcome, {0}", _mainModel.User.LoginName);

                return string.Empty;
            }
        }

        public IList<Session> RecentSessions
        { get { return _mainModel.RecentSessions; } }
    }
}
