using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.Models
{
    public class AccuracyModel
    {
        public decimal Accuracy { get; set; }
        public double NumWords { get; set; }
        public double NumChars { get; set; }
        public double NumCorrectChars { get; set; }
        public double NumIncorrectChars { get; set; }
        public double NumExtraChars { get; set; }
        public double NumShortChars { get; set; }

        //used for the all time model to reflect the total
        //number of accuracies this was computed from
        public int NumEntitiesRepresented { get; set; }

        public static AccuracyModel Create(Data.AnalysisAccuracy orig)
        {
            return new AccuracyModel() {
                Accuracy = orig.Accuracy,
                NumChars = orig.NumChars,
                NumCorrectChars = orig.NumCorrectChars,
                NumExtraChars = orig.NumExtraChars,
                NumIncorrectChars = orig.NumIncorrectChars,
                NumShortChars = orig.NumShortChars,
                NumWords = orig.NumWords,
                NumEntitiesRepresented = 1
            };
        }
    }
}