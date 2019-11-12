using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web.Models
{
    public class UserHistoryItemModel
    {
        public UserHistoryItemModel()
        {
            //this.KeySequences = new List<UserHistoryKeySequenceItemModel>();
        }

        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public int SequenceCount { get; set; }

        /* for a heirarchal result set*/
        //public int SequenceCount { get { return this.KeySequences.Count; } }
        //public IList<UserHistoryKeySequenceItemModel> KeySequences { get; set; }

        public Guid KeySequenceId { get; set; }
        public string SourceKey { get; set; }
        public string SourceType { get; set; }
    }
}