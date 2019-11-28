using KeyCdr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.History
{
    public class AllTimeStatsCalculator
    {
        public AllTimeStatsCalculator()
        { }

        public AllTimeStatsCalculator(Data.KCUser user)
        {
            _user = user;
        }

        private Data.KCUser _user;
        private IList<Data.Session> _sessionsByUser;

        public IList<Data.Session> GetUsersSessions()
        {
            if (_sessionsByUser == null)
                _sessionsByUser = new UserSessionHistory().GetHistoryDetailsAllTime(_user);

            return _sessionsByUser;
        }

        public SpeedModel GetSpeedAllTime()
        {
            IList<Data.Session> sessions = GetUsersSessions();

            var existingSpeedAnalytics = sessions
                .SelectMany(s => s.KeySequence
                .SelectMany(ks => ks.KeySequenceAnalysis
                    .Where(ksa => ksa.AnalysisTypeId.Equals((int)Analytics.AnalyticType.Speed))
                .Select(speed => speed.AnalysisSpeed)));

            if (existingSpeedAnalytics.Count() == 0)
                return null;

            //group by a constant lets you average all the properties
            //in a single iteration...weird
            var totals = existingSpeedAnalytics
                .GroupBy(a => 1)
                .Select(a => new SpeedModel(){
                    CharsPerSec = a.Average(s => s.CharsPerSec),
                    WordPerMin = a.Average(s => s.WordPerMin),
                    TotalTimeInMilliSec = a.Average(s => s.TotalTimeInMilliSec),
                    NumEntitiesRepresented = existingSpeedAnalytics.Count()
                })
                .Single();

            return totals;
        }

        public AccuracyModel GetAccuracyAllTime()
        {
            IList<Data.Session> sessions = GetUsersSessions();

            var existing = sessions
                .SelectMany(s => s.KeySequence
                .SelectMany(ks => ks.KeySequenceAnalysis
                    .Where(ksa => ksa.AnalysisTypeId.Equals((int)Analytics.AnalyticType.Accuracy))
                .Select(accuracy => accuracy.AnalysisAccuracy)));

            if (existing.Count() == 0)
                return null;

            //group by a constant lets you average all the properties
            //in a single iteration...weird
            var totals = existing
                .GroupBy(a => 1)
                .Select(a => new AccuracyModel() {
                    Accuracy = a.Average(acc => acc.Accuracy),
                    NumChars = a.Average(acc => acc.NumChars),
                    NumCorrectChars = a.Average(acc => acc.NumCorrectChars),
                    NumExtraChars = a.Average(acc => acc.NumExtraChars),
                    NumIncorrectChars = a.Average(acc => acc.NumIncorrectChars),
                    NumWords = a.Average(acc => acc.NumWords),
                    NumShortChars = a.Average(acc => acc.NumShortChars),
                    NumEntitiesRepresented = existing.Count()
                })
                .Single();

            return totals;
        }
    }
}
