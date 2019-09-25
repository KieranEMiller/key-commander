using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public class AnalyticData
    {
        public string TextShown { get; set; }
        public string TextEntered { get; set; }
        public TimeSpan Elapsed { get; set; }
    }
}
