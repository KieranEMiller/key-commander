using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public class Speed : BaseAnalytic, IAnalytic
    {
        public Speed(AnalyticData data)
            : base(data)
        { }

        public override AnalyticType GetAnalyticType()
        { return AnalyticType.Speed; }

        private double _wpm;
        public double WordsPerMinute {
            get {
                return Math.Round(_wpm, Constants.PRECISION_FOR_DECIMALS);
            }
            set { _wpm = value; }
        }

        private double _charsPerSec;
        public double CharsPerSecond {
            get {
                return Math.Round(_charsPerSec, Constants.PRECISION_FOR_DECIMALS);
            }
            set { _charsPerSec = value; }
        }

        public TimeSpan TotalTime { get; set; }

        public void Compute()
        {
            this.TotalTime = base._analyticData.Elapsed;
            this.WordsPerMinute = ComputeWordsPerMinute(base._analyticData.TextEntered, base._analyticData.Elapsed.TotalMinutes);
            this.CharsPerSecond = ComputeCharsPerSecond(base._analyticData.TextEntered, base._analyticData.Elapsed.TotalSeconds);
        }

        public override string GetResultSummary()
        {
            return string.Format("TotalTime (ms): {0}, WPM: {1}, Char/s: {2}"
                , Math.Round(this.TotalTime.TotalMilliseconds, Constants.PRECISION_FOR_DECIMALS)
                , Math.Round(this.WordsPerMinute, Constants.PRECISION_FOR_DECIMALS)
                , Math.Round(this.CharsPerSecond, Constants.PRECISION_FOR_DECIMALS)
            );
        }

        private double ComputeWordsPerMinute(string allWords, double minutes)
        {
            int wordCount = allWords.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            return wordCount / minutes;
        }

        private double ComputeCharsPerSecond(string allText, double seconds)
        {
            return allText.Length / seconds;
        }
    }
}
