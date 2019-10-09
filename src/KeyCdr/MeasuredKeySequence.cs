using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Analytics;

namespace KeyCdr
{
    public class MeasuredKeySequence
    {
        public MeasuredKeySequence(string sequence)
            : this(sequence, null)
        { }

        public MeasuredKeySequence(string sequence, AnalyticType analytic)
            : this(sequence, new List<AnalyticType>() { analytic })
        { }

        public MeasuredKeySequence(string sequence, IList<AnalyticType> analysesToRun)
        {
            this.Sequence = sequence;
            this.Analysis = new List<IAnalytic>();
            _stopwatch = new Stopwatch();

            //use speed and accuracy as the default
            if(analysesToRun == null || analysesToRun.Count == 0)
                _analysesToRun = new List<AnalyticType>() { AnalyticType.Speed, AnalyticType.Accuracy };
            else 
                _analysesToRun = analysesToRun;
        }

        private Stopwatch _stopwatch;
        private IList<AnalyticType> _analysesToRun;

        public string Sequence { get; set; }
        public IList<IAnalytic> Analysis { get; private set; }

        public void Start()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public IList<IAnalytic> Peek(string enteredText)
        {
            IList<IAnalytic> analyses = new List<IAnalytic>();
            AnalyticData data = new AnalyticData
            {
                TextShown = this.Sequence,
                TextEntered = enteredText,
                Elapsed = _stopwatch.Elapsed
            };

            foreach (var analyticType in _analysesToRun)
            {
                var analysis = BaseAnalytic.Create(analyticType, data);
                analysis.Compute();
                analyses.Add(analysis);
            }

            return analyses;
        }

        public IList<IAnalytic> Stop(string enteredText)
        {
            _stopwatch.Stop();

            AnalyticData data = new AnalyticData
            {
                TextShown = this.Sequence,
                TextEntered = enteredText,
                Elapsed = _stopwatch.Elapsed
            };

            foreach (var analyticType in _analysesToRun)
            {
                var analysis = BaseAnalytic.Create(analyticType, data);
                analysis.Compute();
                this.Analysis.Add(analysis);
            }

            return this.Analysis;
        }
    }
}
