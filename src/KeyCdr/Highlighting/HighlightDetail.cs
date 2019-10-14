using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Highlighting
{
    public class HighlightDetail
    {
        public int IndexStart { get; set; }
        public int IndexEnd { get; set; }
        public HighlightType HighlightType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            return Equals((HighlightDetail)obj);
        }

        public bool Equals(HighlightDetail obj)
        {
            return (this.IndexStart == obj.IndexStart
                && this.IndexEnd == obj.IndexEnd
                && this.HighlightType == obj.HighlightType);
        }

        public override int GetHashCode()
        {
            return new { this.IndexStart, this.IndexEnd, this.HighlightType }.GetHashCode();
        }
    }
}
