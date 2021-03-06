﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public class AccuracyMeasurement
    {
        public string TextShown { get; set; }
        public string TextEntered { get; set; }

        public int LengthMeasured { get; set; }

        public int NumShortChars { get; set; }
        public int NumExtraChars { get; set; }
        public int NumCorrectChars { get; set; }
        public IList<AccuracyIncorrectChar> IncorrectChars { get; set; }

        public int IndexInLargerText { get; set; }

        public double GetAccuracy() {
            //edge case: if the strings are empty, then accuracy...is 100%? 
            if (this.LengthMeasured == 0)
                return 1.0;

            //note: length measured varies depending on if there are
            //more or less characters entered by the user;
            //see Accuracy.cs comments
            double accuracy =
                (this.LengthMeasured 
                - this.IncorrectChars.Count 
                - this.NumExtraChars 
                - this.NumShortChars)
                / (double)this.LengthMeasured;

            return Math.Round(accuracy, Constants.PRECISION_FOR_DECIMALS);
        }
    }
}
