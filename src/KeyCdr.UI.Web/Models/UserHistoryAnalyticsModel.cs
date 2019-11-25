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

        public int AccuracyMeasurementCount { get { return this.AccuracyMeasurements.Count; } }
        public IList<AccuracyModel> AccuracyMeasurements { get; set; }
    }
}