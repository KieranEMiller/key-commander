using KeyCdr.Data;
using KeyCdr.TextSamples;
using KeyCdr.UI.WPF.Models;
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
    public class TextSequenceInputViewModel: BaseViewModel, INotifyPropertyChanged
    {
        public TextSequenceInputViewModel()
            : this(new KCUser(), new WikipediaTextGenerator())
        { }

        public TextSequenceInputViewModel(KCUser user, ITextSampleGenerator gen)
        {
            _seqInputModel = new TextSequenceInputModel();
            _seqActive = false;

            _generator = gen;
            _sessionMgr = new UserSession(user, gen);
        }

        private bool _seqActive;
        private TextSequenceInputModel _seqInputModel;

        private ITextSampleGenerator _generator;
        private UserSession _sessionMgr;

        private Stopwatch _elapsed;
        private DispatcherTimer _elapsedTimer;

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
            this.TextShown = sample.GetText();
            _sessionMgr.StartNewSequence(sample);

            _elapsed = new Stopwatch();
            _elapsedTimer = new DispatcherTimer(DispatcherPriority.Normal, App.Current.Dispatcher);
            _elapsedTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            _elapsedTimer.Tick += (_, a) =>
            {
                _seqInputModel.Elapsed = _elapsed.Elapsed.ToString("c");
                RaisePropertyChanged("Elapsed");
            };
            _elapsed.Start();
            _elapsedTimer.Start();
        }

        public void StopSequence()
        {
            _elapsed.Stop();
            _elapsedTimer.Stop();
            _sessionMgr.Stop(_seqInputModel.TextEntered);
        }

        public string Elapsed {
            get {
                return string.Format("Elapsed: {0}", _seqInputModel.Elapsed);
            }
            set {
                _seqInputModel.Elapsed = value;
            }
        }

        public string TextShown {
            get {
                return _seqInputModel.TextShown;
            }
            set {
                _seqInputModel.TextShown = value;
                RaisePropertyChanged("TextShown");
            }
        }

        public string TextEntered {
            get { return _seqInputModel.TextEntered; }
            set { _seqInputModel.TextEntered = value; }
        }
    }
}
