using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web.Models
{
    public class UserHistoryKeySequenceItemModel
    {
        public Guid KeySequenceId { get; set; }
        public DateTime Created { get; set; }
        public string SourceKey { get; set; }
        public string SourceType { get; set; }
    }
}