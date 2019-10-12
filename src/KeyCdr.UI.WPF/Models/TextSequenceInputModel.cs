using KeyCdr.UI.WPF.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.UI.WPF.Models
{
    public class TextSequenceInputModel : BasePropertyChanged, INotifyPropertyChanged
    {
        public TextSequenceInputModel()
        {
            _analyticSpeed = new Analytics.Speed();
            _analyticAccuracy = new Analytics.Accuracy();
        }

        private Analytics.Speed _analyticSpeed;
        private Analytics.Accuracy _analyticAccuracy;

        private string _textShown;
        private string _textEntered;

        public Analytics.Speed AnalyticSpeed {
            get { return _analyticSpeed; }
            set {
                _analyticSpeed = value;
                RaisePropertyChanged(nameof(this.AnalyticSpeed));
                RaisePropertyChanged(nameof(this.AccuracyExtraAndShortChars));
            }
        }

        public Analytics.Accuracy AnalyticAccuracy {
            get { return _analyticAccuracy; }
            set {
                _analyticAccuracy = value;
                RaisePropertyChanged(nameof(this.AnalyticAccuracy));
            }
        }

        public string TextShown {
            get { return _textShown; }
            set {
                _textShown = value;
                RaisePropertyChanged(nameof(this.TextShown));
            }
        }

        public string TextEntered {
            get { return _textEntered; }
            set {
                _textEntered = value;
                RaisePropertyChanged(nameof(this.TextEntered));
            }
        }

        public string AccuracyExtraAndShortChars {
            get {
                return string.Format("{0}/{1}", 
                    _analyticAccuracy.NumExtraChars, 
                    _analyticAccuracy.NumShortChars
                );
            }
        }
    }
}
