using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.Models
{
    public class AccuracyModel : BaseModel
    {
        //these values are used for all time stats, hence the reason 
        //for floating point instead of ints
        private decimal _accuracy;
        private double _numWords;
        private double _numChars;
        private double _numCorrectChars;
        private double _numIncorrectChars;
        private double _numExtraChars;
        private double _numShortChars;
        
        public decimal Accuracy {
            get { return _accuracy; }
            set { _accuracy = base.ToPrecision(value);  }
        }

        public double NumWords {
            get { return _numWords; }
            set { _numWords = base.ToPrecision(value);  }
        }

        public double NumChars
        {
            get { return _numChars; }
            set { _numChars = base.ToPrecision(value); }
        }

        public double NumCorrectChars
        {
            get { return _numCorrectChars; }
            set { _numCorrectChars = base.ToPrecision(value); }
        }

        public double NumIncorrectChars
        {
            get { return _numIncorrectChars; }
            set { _numIncorrectChars = base.ToPrecision(value); }
        }

        public double NumExtraChars
        {
            get { return _numExtraChars; }
            set { _numExtraChars = base.ToPrecision(value); }
        }

        public double NumShortChars {
            get { return _numShortChars; }
            set { _numShortChars = base.ToPrecision(value); }
        }

        //used for the all time model to reflect the total
        //number of accuracies this was computed from
        public int NumEntitiesRepresented { get; set; }

        public AccuracyModel Create(Data.AnalysisAccuracy orig)
        {
            return new AccuracyModel() {
                Accuracy = base.ToPrecision(orig.Accuracy),
                NumChars = base.ToPrecision((double)orig.NumChars),
                NumCorrectChars = base.ToPrecision((double)orig.NumCorrectChars),
                NumExtraChars = base.ToPrecision((double)orig.NumExtraChars),
                NumIncorrectChars = base.ToPrecision((double)orig.NumIncorrectChars),
                NumShortChars = base.ToPrecision((double)orig.NumShortChars), 
                NumWords = base.ToPrecision((double)orig.NumWords), 
                NumEntitiesRepresented = 1
            };
        }
    }
}