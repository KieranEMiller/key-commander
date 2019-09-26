using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public interface IAnalytic
    {
        void Compute();

        AnalyticType GetAnalyticType();

        string GetTextShown();
        string GetTextEntered();
        string GetResultSummary();
    }
}
