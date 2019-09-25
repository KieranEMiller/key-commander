using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public class Accuracy : BaseAnalytic, IAnalytic
    {
        public Accuracy(AnalyticData data)
            : base(data)
        { }

        public override AnalyticType GetAnalyticType()
        { return AnalyticType.Accuracy; }

        public void Compute()
        {

        }
    }
}
