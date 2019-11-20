using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Analytics;

namespace KeyCdr.KeySequences
{
    public class MeasuredKeySequence : IMeasuredKeySequence
    {
        public MeasuredKeySequence()
            : this(null)
        { }

        public MeasuredKeySequence(AnalyticType analytic)
            : this(new List<AnalyticType>() { analytic })
        { }

        public MeasuredKeySequence(List<AnalyticType> analysesToRun)
        {
            this.Analysis = new List<IAnalytic>();
            _stopwatch = new Stopwatch();

            //use speed and accuracy as the default
            if(analysesToRun == null || analysesToRun.Count == 0)
                _analysesToRun = new List<AnalyticType>() { AnalyticType.Speed, AnalyticType.Accuracy };
            else 
                _analysesToRun = analysesToRun;
        }

        protected Stopwatch _stopwatch;
        protected IList<AnalyticType> _analysesToRun;

        public string Sequence { get; set; }
        public IList<IAnalytic> Analysis { get; private set; }

        public virtual void Start(string sequence)
        {
            this.Sequence = sequence;

            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public IList<IAnalytic> Peek(string enteredText)
        {
            return Stop(enteredText, _stopwatch.Elapsed);
        }

        public IList<IAnalytic> Peek(string enteredText, TimeSpan elapsed)
        {
            return Stop(enteredText, elapsed);
        }

        public virtual IList<IAnalytic> Stop(string enteredText)
        {
            _stopwatch.Stop();

            return Stop(enteredText, _stopwatch.Elapsed);
        }

        public virtual IList<IAnalytic> Stop(string enteredText, TimeSpan elapsed)
        {
            AnalyticData data = new AnalyticData
            {
                TextShown = this.Sequence,
                TextEntered = enteredText,
                Elapsed = elapsed
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
