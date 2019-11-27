using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;

namespace KeyCdr.History
{
    public class UserSessionHistory : BaseDBAccess
    {
        public const int DEFAULT_ALL_HISTORY_NUM_RECORDS = 30;

        public UserSessionHistory()
        { }

        public IList<Data.Session> GetSessionsByUser(Data.KCUser currentUser)
        {
            var results = _db.Session
                .Where(sess => sess.UserId.Equals(currentUser.UserId))
                .Include(sess =>sess.KeySequence)
                .Include(sess=>sess.KeySequence.Select(b=>b.SourceType))
                .OrderByDescending(s => s.Created);

            return results.ToList();
        }

        public Data.KeySequence GetHistoryDetailsByKeySequence(Guid keySequenceId)
        {
            var result = _db.KeySequence
                .Where(ks => ks.KeySequenceId.Equals(keySequenceId))
                .Include(ks => ks.SourceType)
                .Include(ksa => ksa.KeySequenceAnalysis)
                .Include(ksa => ksa.KeySequenceAnalysis.Select(a => a.AnalysisType))
                .Include(ksa => ksa.KeySequenceAnalysis.Select(a => a.AnalysisAccuracy))
                .Include(ksa => ksa.KeySequenceAnalysis.Select(a => a.AnalysisSpeed))
                .FirstOrDefault();

            return result;
        }

        public IList<Data.Session> GetHistoryDetailsAllTime(KCUser currentUser)
        {
            return GetHistoryDetailsAllTime(currentUser, DEFAULT_ALL_HISTORY_NUM_RECORDS);
        }

        public IList<Data.Session> GetHistoryDetailsAllTime(KCUser currentUser, int lastXNumberRecords)
        {
            var results = _db.Session
                .Where(sess => sess.UserId.Equals(currentUser.UserId))
                .Include(sess =>sess.KeySequence)
                .Include(sess=>sess.KeySequence.Select(b=>b.SourceType))
                .Include(sess=>sess.KeySequence.Select(b=>b.KeySequenceAnalysis))
                .Include(sess=>sess.KeySequence.Select(b=>b.KeySequenceAnalysis.Select(c=>c.AnalysisSpeed)))
                .Include(sess=>sess.KeySequence.Select(b=>b.KeySequenceAnalysis.Select(c=>c.AnalysisAccuracy)))
                .OrderByDescending(s => s.Created)
                .Take(lastXNumberRecords);

            return results.ToList();
        }
    }
}
