using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public abstract class BaseAnalytic
    {
        public BaseAnalytic(AnalyticData data)
        {
            _analyticData = data;
        }

        public AnalyticData _analyticData;

        public string GetTextShown()
        {
            return _analyticData.TextShown;
        }

        public string GetTextEntered()
        {
            return _analyticData.TextEntered ;
        }

        public abstract AnalyticType GetAnalyticType();

        public virtual string GetResultSummary()
        {
            return string.Empty;
        }

        public static IAnalytic Create(AnalyticType type, AnalyticData data)
        {
            if (type == AnalyticType.Speed) return new Speed(data);
            else if (type == AnalyticType.Accuracy) return new Accuracy(data);

            throw new NotImplementedException();
        }
    }
}
