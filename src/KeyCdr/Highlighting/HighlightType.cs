using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Highlighting
{
    public enum HighlightType
    {
        IncorrectChar = 0,
        ExtraChars = 1,
        ShortChars = 2,
        Normal = 3,
        Unevaluated = 4
    }
}
