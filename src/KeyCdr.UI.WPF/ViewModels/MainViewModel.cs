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
using KeyCdr.UI.WPF.Util;

namespace KeyCdr.UI.WPF.ViewModels
{
    public class MainViewModel : BasePropertyChanged, INotifyPropertyChanged
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
        public MainModel Model { get { return _mainModel; } }

        public void RefreshRecentSessions()
        {
            _mainModel.RecentSessions = new KeyCdr.History.UserSessionHistory().GetSessionsByUser(_mainModel.User);
            RaisePropertyChanged(nameof(this.RefreshRecentSessions));
        }

        public IEnumerable<TextSampleSourceType> NewSessionSourceTypes
        {
            get {
                return Enum.GetValues(typeof(TextSampleSourceType))
                    .Cast<TextSampleSourceType>();
            }
        }

        public void StartNewSession()
        {
            TextSequenceInputViewModel vm = new TextSequenceInputViewModel(_mainModel.User, new WikipediaTextGeneratorWithLocalCache());
            Windows.TextSequenceInputWindow textSeq = new Windows.TextSequenceInputWindow(vm);
            textSeq.ShowDialog();

            RefreshRecentSessions();
        }
    }
}
