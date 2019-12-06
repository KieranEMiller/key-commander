using KeyCdr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web.Models
{
    public class UserHistoryAnalyticsModel 
    {
        public UserHistoryAnalyticsModel()
        {
            this.SpeedMeasurements = new List<SpeedModel>();
            this.AccuracyMeasurements = new List<AccuracyModel>();
        }

        public int SpeedMeasurementCount { get { return this.SpeedMeasurements.Count; } }
        public IList<SpeedModel> SpeedMeasurements { get; set; }

        public DateTime SpeedDateFrom { get {
                return this.SpeedMeasurements.Min(s => s.CreateDate);
            }
        }

        public DateTime SpeedDateTo { get {
                return this.SpeedMeasurements.Max(s => s.CreateDate);
            }
        }

        public int AccuracyMeasurementCount { get { return this.AccuracyMeasurements.Count; } }
        public IList<AccuracyModel> AccuracyMeasurements { get; set; }

        public DateTime AccuracyDateFrom { get {
                return this.AccuracyMeasurements.Min(a => a.CreateDate);
            }
        }

        public DateTime AccuracyDateTo { get {
                return this.AccuracyMeasurements.Max(a => a.CreateDate);
            }
        }
    }
}