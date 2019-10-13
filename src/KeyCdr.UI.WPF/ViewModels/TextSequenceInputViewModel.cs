using KeyCdr.Analytics;
using KeyCdr.Data;
using KeyCdr.TextSamples;
using KeyCdr.UI.WPF.Models;
using KeyCdr.UI.WPF.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _sessionMgr.Stop(_seqInputModel.TextEntered);
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
    }
}
