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

            _updateTimer = new DispatcherTimer(DispatcherPriority.Normal, App.Current.Dispatcher);
            _updateTimer.Interval = new TimeSpan(0, 0, 0, 0, UPDATE_INTERVAL_IN_MS);
            _updateTimer.Tick += (_, a) => {
                 DoInterval();
            };
        }

        private bool _seqActive;
        private TextSequenceInputModel _seqInputModel;

        private ITextSampleGenerator _generator;
        private UserSession _sessionMgr;

        private DispatcherTimer _updateTimer;

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
            _seqInputModel.TextShown = string.Empty;
            _seqInputModel.TextEntered = string.Empty;
            _seqInputModel.InlineList = new ObservableCollection<Inline>();

            ITextSample sample = _sessionMgr.GetSentence();
            _seqInputModel.TextShown = sample.GetText();

            _sessionMgr.StartNewSequence(sample);
            _updateTimer.Start();
        }

        public void StopSequence()
        {
            _updateTimer.Stop();
            var analytics = _sessionMgr.Stop(_seqInputModel.TextEntered);

            IAnalytic accuracy = analytics
                .Where(a => a.GetAnalyticType() == AnalyticType.Accuracy)
                .FirstOrDefault();

            ShowResults(accuracy as Accuracy);
        }

        public void DoInterval()
        {
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
            var inlines = ConvertHighlightsToInlines(_seqInputModel.TextShown, accuracy);
            _seqInputModel.InlineList = new ObservableCollection<Inline>(inlines);
        }

        public IList<Inline> ConvertHighlightsToInlines(string origText, Accuracy accuracy)
        {
            HighlightDeterminator highlighter = new HighlightDeterminator();
            IList<HighlightDetail> details = highlighter.Compute(accuracy);

            HighlightedTextBuilder highlightBuilder = new HighlightedTextBuilder();
            Queue<HighlightedText> highlightedSections = highlightBuilder.Compute(_seqInputModel.TextShown, details);

            IList<Inline> inlines = new List<Inline>();
            while(highlightedSections.Count > 0)
            {
                var section = highlightedSections.Dequeue();
                Inline newSection = new Run(section.Text);
                newSection.Foreground = new Util.HighlightBrushFactory().HighlightToBrush(section.HighlightType);
                if (section.HighlightType != HighlightType.Normal)
                    newSection.FontWeight = System.Windows.FontWeights.Bold;

                inlines.Add(newSection);
            }
            return inlines;
        }
    }
}
