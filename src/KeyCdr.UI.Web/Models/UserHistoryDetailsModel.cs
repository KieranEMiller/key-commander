using KeyCdr.Analytics;
using KeyCdr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web.Models
{
    public class UserHistoryDetailsModel
    {
        public string TextShown { get; set; }
        public string TextEntered { get; set; }

        public string SourceKey { get; set; }
        public string SourceType { get; set; }

        public AccuracyModel AnalysisAccuracy { get; set; }
        public AccuracyModel AnalysisAccuracyAllTime { get; set; }

        public SpeedModel AnalysisSpeed { get; set; }
        public SpeedModel AnalysisSpeedAllTime { get; set; }

        public static UserHistoryDetailsModel Create(Data.KeySequence keySequence)
        {
            var result = new UserHistoryDetailsModel() {
                TextEntered = keySequence.TextEntered,
                TextShown = keySequence.TextShown,
                SourceKey = keySequence.SourceKey,
                SourceType = keySequence.SourceType.Name,
            };

            var speedAnalysis = keySequence.KeySequenceAnalysis
                .Where(k => k.AnalysisTypeId.Equals((int)AnalyticType.Speed))
                .FirstOrDefault();

            if(speedAnalysis != null) {
                result.AnalysisSpeed = SpeedModel.Create(speedAnalysis.AnalysisSpeed);
            }
                
            var accuracyAnalysis = keySequence.KeySequenceAnalysis
                .Where(k => k.AnalysisTypeId.Equals((int)AnalyticType.Accuracy))
                .FirstOrDefault();
            
            if(accuracyAnalysis != null) {
                result.AnalysisAccuracy = AccuracyModel.Create(accuracyAnalysis.AnalysisAccuracy);
            }

            return result;
        }
    }
}