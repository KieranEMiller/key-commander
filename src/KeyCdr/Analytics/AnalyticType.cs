using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public enum AnalyticType
    {
        Accuracy,

        //in words per minute (WPM)
        Speed,

        //how difficult was the sample text; difficult is subjective 
        //but should mean variety of keys and reach
        Difficulty
    }
}
