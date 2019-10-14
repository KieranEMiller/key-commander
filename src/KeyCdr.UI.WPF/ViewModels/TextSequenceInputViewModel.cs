using KeyCdr.Analytics;
using KeyCdr.Data;
using KeyCdr.Highlighting;
using KeyCdr.TextSamples;
using KeyCdr.UI.WPF.Models;
using KeyCdr.UI.WPF.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace KeyCdr.UI.WPF.ViewModels
{
    public class TextSequenceInputViewModel
    {
        private const int UPDATE_INTERVAL_IN_MS = 250;

        public TextSequenceInputViewModel()
            : this(new KCUser(), new WikipediaTextGenerator())
        { }

        public TextSequenceInputViewModel(KCUser user, ITextSampleGenerator gen)
        {
            _seqInputModel = new TextSequenceInputModel();
            _seqActive = false;

            _generator = gen;
            _sessionMgr = new UserSession(user, gen);

            _elapsedTimer = new DispatcherTimer(DispatcherPriority.Normal, App.Current.Dispatcher);
            _elapsedTimer.Interval = new TimeSpan(0, 0, 0, 0, UPDATE_INTERVAL_IN_MS);
            _elapsedTimer.Tick += (_, a) => {
                 DoInterval();
            };
        }

        private bool _seqActive;
        private TextSequenceInputModel _seqInputModel;

        private ITextSampleGenerator _generator;
        private UserSession _sessionMgr;

        private DispatcherTimer _elapsedTimer;

        public TextSequenceInputModel Model { get { return _seqInputModel; } }

        public void RegisterKeyDown(KeyEventArgs keyArgs)
        {
            if(keyArgs.Key == Key.Enter)
            {
                if (_seqActive)
                    StopSequence();
                else
                    StartSequence();

                _seqActive = !_seqActive;
            }
        }

        public void StartSequence()
        {
            ITextSample sample = _sessionMgr.GetSentence();
            _seqInputModel.TextShown = sample.GetText();
            _sessionMgr.StartNewSequence(sample);

            _seqInputModel.TextEntered = string.Empty;

            _elapsedTimer.Start();
        }

        public void StopSequence()
        {
            _elapsedTimer.Stop();
            var analytics = _sessionMgr.Stop(_seqInputModel.TextEntered);

            ShowResults(analytics.Select(a=>a.GetAnalyticType() == AnalyticType.Accuracy) as Accuracy);
        }

        public void DoInterval()
        {
            if (string.IsNullOrWhiteSpace(_seqInputModel.TextEntered))
                return;

            IList<IAnalytic> analyses = _sessionMgr.Peek(_seqInputModel.TextEntered);
            foreach(IAnalytic analytic in analyses)
            {
                if(analytic is Speed)
                {
                    var speed = (Speed)analytic;
                    _seqInputModel.AnalyticSpeed = speed;
                }
                else if(analytic is Accuracy)
                {
                    var accuracy = (Accuracy)analytic;
                    _seqInputModel.AnalyticAccuracy = accuracy;
                }
            }
        }

        public void ShowResults(Accuracy accuracy)
        {
            HighlightDeterminator highlighter = new HighlightDeterminator();
            IList<HighlightDetail> details = highlighter.Compute(accuracy);

            var inlines = ConvertHighlightsToInlines(_seqInputModel.TextShown, details);
            _seqInputModel.InlineValues = new ObservableCollection<Inline>(inlines);
        }

        public IList<Inline> ConvertHighlightsToInlines(string origText, IList<HighlightDetail> details)
        {
            List<Inline> inlines = new List<Inline>();

            int lastIndex = 0;
            foreach(HighlightDetail highlight in details.OrderBy(d=>d.IndexStart))
            {
                Inline nextBlockUnformatted = new Run(origText.Substring(lastIndex, highlight.IndexStart));
                inlines.Add(nextBlockUnformatted);

                Inline formatted = new Run(origText.Substring(highlight.IndexStart, highlight.IndexEnd));
                formatted.Foreground = new HighlightBrushFactory().HighlightToBrush(highlight.HighlightType);
                inlines.Add(formatted);
            }

            return inlines;
        }
    }
}
