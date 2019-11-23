using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KeyCdr.Models
{
    public class SpeedModel : BaseModel
    {
        private decimal _totalTimeInMs;
        private decimal _wordPerMin;
        private decimal _charsPerSec;

        public decimal TotalTimeInMilliSec {
            get { return _totalTimeInMs; }
            set { _totalTimeInMs = base.ToPrecision(value);  }
        }

        public string TotalTimeDisplayFriendly {
            get { return this.MillisecToFriendly(_totalTimeInMs); }
        }

        public decimal WordPerMin {
            get { return _wordPerMin; }
            set { _wordPerMin = base.ToPrecision(value);  }
        }

        public decimal CharsPerSec {
            get { return _charsPerSec; }
            set { _charsPerSec = base.ToPrecision(value);  }
        }
        
        //used for the all time model to reflect the total
        //number of accuracies this was computed from
        public int NumEntitiesRepresented { get; set; }

        public string MillisecToFriendly(decimal millisec)
        {
            TimeSpan span = TimeSpan.FromMilliseconds((double)millisec);
            StringBuilder friendlyTime = new StringBuilder();
            friendlyTime.AppendFormat(
                "{0:D2}m {1:D2}s {2:D3}ms"
                , span.Minutes, span.Seconds, span.Milliseconds
            );
            friendlyTime.AppendFormat(" ({0:n0}ms total)", span.TotalMilliseconds);
            return friendlyTime.ToString();
        }

        public SpeedModel Create(Data.AnalysisSpeed orig)
        {
            return new SpeedModel() {
                CharsPerSec = base.ToPrecision(orig.CharsPerSec),
                TotalTimeInMilliSec = base.ToPrecision(orig.TotalTimeInMilliSec),
                WordPerMin = base.ToPrecision(orig.WordPerMin),
                NumEntitiesRepresented = 1,
            };
        }
    }
}