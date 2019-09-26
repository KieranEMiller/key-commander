using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    //TODO: other accuracy variables to consider for implementation
    //- should case sensitivity be ignored?  probably not, case is one measure of accuracy
    //- investigate other options for complex likeness through established algorithms
    public class Accuracy : BaseAnalytic, IAnalytic
    {
        public Accuracy(AnalyticData data)
            : base(data)
        { }

        public override AnalyticType GetAnalyticType()
        { return AnalyticType.Accuracy; }

        public double AccuracyVal { get; set; }
        public int IncorrectChars { get; set; }
        public int ShortChars { get; set; }
        public int ExtraChars { get; set; }

        public void Compute()
        {
            string shown = this._analyticData.TextShown;
            string entered = this._analyticData.TextEntered;

            //init cost to the difference in length between the two strings
            //if the entered string is too short or too long both should be counted as 'wrong'		
            int wrongChars = 0;
            int numCharsToCompare = Math.Min(shown.Length, entered.Length);

            //loop over the shortest length
            for (int i = 0; i < numCharsToCompare; i++)
            {
                if (shown[i] != entered[i])
                    wrongChars++;
            }

            int lengthDiff = entered.Length - shown.Length;
            int extraChars = (lengthDiff > 0) ? lengthDiff : 0;
            int shortChars = (lengthDiff < 0) ? Math.Abs(lengthDiff) : 0;

            //TODO decide which measurement type to use
            //method 1:
            //calculate the accuracy relative to the expected length; this ensures that 
            //having X too many characters gets you the same accuracy as X too few characters
            //ex: expected 'test', input 'te' and 'testxy' should have the same accuracy
            //double accuracy = (shown.Length - wrongChars - Math.Abs(lengthDiff)) / (double)shown.Length;

            //method 2:
            //calculate the accuracy differently based on if there are too few character or two 
            //many characters.  as in the previous example:
            //expected 'test', input 'te' and 'testxy'; we want the later to have a better accuracy
            //since they did get 4 matches so 4/6 total vs 2/4
            double accuracy = (shown.Length - wrongChars) / (double)shown.Length;
            if(shortChars > 0)
                accuracy = (shown.Length - wrongChars - shortChars) / (double)shown.Length;

            if(extraChars > 0)
                accuracy = entered.Length - wrongChars / (double)entered.Length;

            this.AccuracyVal = accuracy;
            this.IncorrectChars = wrongChars;
            this.ShortChars = shortChars;
            this.ExtraChars = extraChars;
        }

        public override string GetResultSummary()
        {
            return string.Format("{0}%: matched {1} incorrectly of {2}; extra: {3}; short: {4}",
                this.AccuracyVal,
                this.IncorrectChars,
                base._analyticData.TextShown.Length,
                this.ExtraChars,
                this.ShortChars
            );
        }
    }
}
