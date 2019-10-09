using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;

namespace KeyCdr.Analytics
{
    //TODO: other accuracy variables to consider for implementation
    //- should case sensitivity be ignored?  probably not, case is one measure of accuracy
    //- investigate other options for complex likeness through established algorithms
    public class Accuracy : BaseAnalytic, IAnalytic, IAnalyticRecordable
    {
        public Accuracy()
            :this(null)
        { }

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
                if (_measurements.Count == 0) return 0;
                return _measurements.Average(m => m.GetAccuracy());
            }
        }

        public int NumWordsEvaluated {
            get {
                return _measurements.Count;
            }
        }

        public int NumCharsEvaluated {
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
            string[] shownWords = this._analyticData.TextShown.Split(Constants.StringSplits.SEPARATOR_WORD);
            string[] enteredWords = this._analyticData.TextEntered.Split(Constants.StringSplits.SEPARATOR_WORD);

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
            int numCharsToCompare = Math.Min(shown.Length, entered.Length);

            IList<AccuracyIncorrectChar> incorrectChars = new List<AccuracyIncorrectChar>();

            //loop over the shortest length
            for (int i = 0; i < numCharsToCompare; i++)
            {
                if (shown[i] != entered[i])
                {
                    incorrectChars.Add(new AccuracyIncorrectChar() {
                        Expected = shown[i],
                        Found=entered[i],
                        Index=i
                    });
                }
            }

            int correctChars = numCharsToCompare - incorrectChars.Count;
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
                TextShown = shown,
                TextEntered = entered,
                LengthMeasured = (lengthDiff > 0) ? entered.Length : shown.Length,
                NumCorrectChars = correctChars,
                NumIncorrectChars = incorrectChars.Count,
                NumShortChars = shortChars,
                NumExtraChars = extraChars,
                IncorrectChars = incorrectChars
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

        public void Record(KeyCommanderEntities db, KeySequenceAnalysis seqAnalysis)
        {
            Data.AnalysisAccuracy accuracy = new AnalysisAccuracy();
            accuracy.AnalysisAccuracyId = Guid.NewGuid();
            accuracy.AnalysisTypeId = (int)this.GetAnalyticType();
            accuracy.KeySequenceAnalysisId = seqAnalysis.KeySequenceAnalysisId;
            accuracy.NumWords = this.NumWordsEvaluated;
            accuracy.NumChars = this.NumCharsEvaluated;
            accuracy.NumCorrectChars = this.NumCorrectChars;
            accuracy.NumIncorrectChars = this.NumIncorrectChars;
            accuracy.NumExtraChars = this.NumExtraChars;
            accuracy.NumShortChars = this.NumShortChars;
            accuracy.Accuracy = (decimal)this.AccuracyVal;

            db.AnalysisAccuracys.Add(accuracy);
        }
    }
}
