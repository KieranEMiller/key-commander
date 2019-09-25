using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public class Speed : BaseAnalytic, IAnalytic
    {
        private const int PRECISION = 2;

        public Speed(AnalyticData data)
            : base(data)
        { }

        public override AnalyticType GetAnalyticType()
        { return AnalyticType.Speed; }

        public double WordsPerMinute { get; set; }
        public double CharsPerSecond { get; set; }

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
                , Math.Round(this.TotalTime.TotalMilliseconds, PRECISION)
                , Math.Round(this.WordsPerMinute, PRECISION)
                , Math.Round(this.CharsPerSecond, PRECISION)
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
