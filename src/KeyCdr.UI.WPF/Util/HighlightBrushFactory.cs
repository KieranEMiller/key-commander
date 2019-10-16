using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KeyCdr.UI.WPF.Util
{
    public class HighlightBrushFactory
    {
        public Brush HighlightToBrush(Highlighting.HighlightType type)
        {
            if (type == Highlighting.HighlightType.ExtraChars)
                return Brushes.Green;

            else if (type == Highlighting.HighlightType.IncorrectChar)
                return Brushes.Red;

            else if (type == Highlighting.HighlightType.ShortChars)
                return Brushes.Orange;

            else
                return Brushes.Black;
        }
    }
}
