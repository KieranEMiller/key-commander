using KeyCdr.Data;
using KeyCdr.TextSamples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KeyCdr.UI.WPF.Models
{
    public class MainModel
    {
        public MainModel()
        { }

        public KCUser User { get; set; }
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
                //return collection;

                ListCollectionView collectionView = new ListCollectionView(collection);
                collectionView.GroupDescriptions.Add(new PropertyGroupDescription("SessionDisplayName"));
                return collectionView;
                //myDataGrid.ItemsSource = collectionView;
            }
        }
    }
}
