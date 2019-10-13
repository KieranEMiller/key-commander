using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public class AccuracyIncorrectChar
    {
        public AccuracyIncorrectChar()
        { }

        public char Expected { get; set; }
        public char Found { get; set; }
        public int Index { get; set; }
    }
}
