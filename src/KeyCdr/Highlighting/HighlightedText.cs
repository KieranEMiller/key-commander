using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Highlighting
{
    public class HighlightedText
    {
        public string Text { get; set; }
        public HighlightType HighlightType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            return Equals((HighlightedText)obj);
        }

        public bool Equals(HighlightedText obj)
        {
            return (this.Text == obj.Text
                && this.HighlightType == obj.HighlightType) ;
        }

        public override int GetHashCode()
        {
            return new { this.Text, this.HighlightType }.GetHashCode();
        }
    }
}
