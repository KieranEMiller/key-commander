using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.Models
{
    public class SpeedModel
    {
        public decimal TotalTimeInMilliSec { get; set; }
        public decimal WordPerMin { get; set; }
        public decimal CharsPerSec { get; set; }
        
        //used for the all time model to reflect the total
        //number of accuracies this was computed from
        public int NumEntitiesRepresented { get; set; }

        public static SpeedModel Create(Data.AnalysisSpeed orig)
        {
            return new SpeedModel() {
                CharsPerSec = orig.CharsPerSec,
                TotalTimeInMilliSec = orig.CharsPerSec,
                WordPerMin = orig.WordPerMin,
                NumEntitiesRepresented = 1,
            };
        }
    }
}