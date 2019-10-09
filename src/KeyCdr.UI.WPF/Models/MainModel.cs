using KeyCdr.Data;
using KeyCdr.TextSamples;
using KeyCdr.UI.WPF.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KeyCdr.UI.WPF.Models
{
    public class MainModel : BasePropertyChanged, INotifyPropertyChanged
    {
        private const TextSampleSourceType DEFAULT_SOURCE_TYPE = TextSampleSourceType.Wikipedia;

        public MainModel()
        {
            SelectedSourceType = DEFAULT_SOURCE_TYPE;
        }

        private KCUser _user;
        private TextSampleSourceType _selectedSourceType;

        public KCUser User {
            get {
                return _user;
            }
            set {
                _user = value;
                RaisePropertyChanged(nameof(this.User));
            }
        }

        public TextSampleSourceType SelectedSourceType
        {
            get { return _selectedSourceType; }
            set {
               _selectedSourceType = value;
                RaisePropertyChanged(nameof(this.SelectedSourceType));
            }
        }

        public IList<Session> RecentSessions { get; set; }
        public ListCollectionView RecentSessionsView
        {
            get {
                var collection = this.RecentSessions
                    .SelectMany(seq => seq.KeySequences
                    .Select(r => new MainModelSessionView
                    {
                        SessionCreated = seq.Created,
                        SequenceCount = seq.KeySequences.Count,
                        SequenceSource = (TextSampleSourceType)Enum.Parse(typeof(TextSampleSourceType), r.SourceType.SourceTypeId.ToString(), true),
                        SequenceCreated = r.Created,
                        SessionId = seq.SessionId,
                        UserId = seq.UserId
                    }))
                    .OrderByDescending(o=>o.SequenceCreated)
                    .ToList();

                ListCollectionView collectionView = new ListCollectionView(collection);
                collectionView.GroupDescriptions.Add(new PropertyGroupDescription("SessionDisplayName"));
                return collectionView;
            }
        }
    }
}
