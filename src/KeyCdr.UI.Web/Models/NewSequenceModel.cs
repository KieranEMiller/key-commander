using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web.Models
{
    public class NewSequenceModel
    {
        public Guid SessionId { get; set; }

        public string Text { get; set; }
        public string SourceKey { get; set; }
        public string SourceType { get; set; }
    }
}