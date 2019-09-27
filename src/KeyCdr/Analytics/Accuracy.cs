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
        {
            _measurements = new List<AccuracyMeasurement>();
        }

        public override AnalyticType GetAnalyticType()
        { return AnalyticType.Accuracy; }

        private List<AccuracyMeasurement> _measurements;

        public double AccuracyVal {
            get {
                return _measurements.Average(m => m.GetAccuracy());
            }
        }

        public double NumWordsEvaluated {
            get {
                return _measurements.Count;
            }
        }

        public double NumCharsEvaluated {
            get {
                return _measurements.Sum(m=>m.LengthMeasured);
            }
        }

        public int NumIncorrectChars {
            get {
                return _measurements.Sum(m => m.NumIncorrectChars);
            }
        }

        public int NumCorrectChars {
            get {
                return _measurements.Sum(m => m.NumCorrectChars);
            }
        }

        public int NumShortChars {
            get {
                return _measurements.Sum(m => m.NumShortChars);
            }
        }
        public int NumExtraChars {
            get {
                return _measurements.Sum(m => m.NumExtraChars);
            }
        }

        public void Compute()
        {
            string[] shownWords = this._analyticData.TextShown.Split(new char[] { ' ' });
            string[] enteredWords = this._analyticData.TextEntered.Split(new char[] { ' ' });

            //TODO: need to be able to handle different numbers of words
            for(int i=0; i < Math.Min(shownWords.Length, enteredWords.Length);i++)
            {
                var measure = ComputeForWord(shownWords[i], enteredWords[i]);
                _measurements.Add(measure);
            }
        }

        public AccuracyMeasurement ComputeForWord(string shown, string entered)
        {
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

            int correctChars = numCharsToCompare - wrongChars;
            int lengthDiff = entered.Length - shown.Length;

            //positive diff => extra characters
            int extraChars = (lengthDiff > 0) ? lengthDiff : 0;

            //negative diff => too few characters
            int shortChars = (lengthDiff < 0) ? Math.Abs(lengthDiff) : 0;

            //reasoning behind using different lengths depending if their are
            //too many or two few characters: you could approach the accuracy calculation 2 ways:

            //method 1:
            //calculate the accuracy relative to the expected or shown length; this ensures that 
            //having X too many characters gets you the same accuracy as X too few characters
            //ex: expected 'test', input 'te' and 'testxy' should have the same accuracy
            //double accuracy = (shown.Length - wrongChars - Math.Abs(lengthDiff)) / (double)shown.Length;

            //method 2:
            //calculate the accuracy differently based on if there are too few character or two 
            //many characters.  as in the previous example:
            //expected 'test', input 'te' and 'testxy'; we want the later to have a better accuracy
            //since they did get 4 matches so 4/6 total vs 2/4
            return new AccuracyMeasurement()
            {
                LengthMeasured = (lengthDiff > 0) ? entered.Length : shown.Length,
                NumCorrectChars = correctChars,
                NumIncorrectChars = wrongChars,
                NumShortChars = shortChars,
                NumExtraChars = extraChars
            };
        }

        public override string GetResultSummary()
        {
            return string.Format("{0}%: matched {1} incorrectly, {5} correctly of {2} chars; extra: {3}; short: {4}",
                this.AccuracyVal*100,
                this.NumIncorrectChars,
                this.NumCharsEvaluated,
                this.NumExtraChars,
                this.NumShortChars,
                this.NumCorrectChars
            );
        }
    }
}
