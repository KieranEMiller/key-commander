using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.UI.WPF.Models;
using KeyCdr.Data;
using System.ComponentModel;
using System.Windows.Data;
using KeyCdr.TextSamples;

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

            RefreshRecentSessions();
        }

        private MainModel _mainModel;

        public void RefreshRecentSessions()
        {
            _mainModel.RecentSessions = new KeyCdr.History.UserSessionHistory().GetSessionsByUser(_mainModel.User);
            RaisePropertyChanged("RecentSessionsView");
        }

        public TextSampleSourceType SelectedSourceType
        {
            get { return _mainModel.SelectedSourceType; }
            set {
                _mainModel.SelectedSourceType = value;
                RaisePropertyChanged("SelectedSourceType");
            }
        }

        public IEnumerable<TextSampleSourceType> NewSessionSourceTypes
        {
            get {
                return Enum.GetValues(typeof(TextSampleSourceType))
                    .Cast<TextSampleSourceType>();
            }
        }

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

        public ListCollectionView RecentSessionsView
        { get { return _mainModel.RecentSessionsView; } }

        public void StartNewSession()
        {
            TextSequenceInputViewModel vm = new TextSequenceInputViewModel(_mainModel.User, new WikipediaTextGeneratorWithLocalCache());
            Windows.TextSequenceInputWindow textSeq = new Windows.TextSequenceInputWindow(vm);
            textSeq.ShowDialog();
            RefreshRecentSessions();
        }
    }
}
