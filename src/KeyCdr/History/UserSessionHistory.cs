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
        public UserSessionHistory()
        { }

        public IList<Data.Session> GetSessionsByUser(Data.KCUser currentUser)
        {
            var results = _db.Sessions
                .Where(sess => sess.UserId.Equals(currentUser.UserId))
                .Include(sess =>sess.KeySequences)
                .Include(sess=>sess.KeySequences.Select(b=>b.SourceType))
                //.Include(sess=>sess.KeySequences.Select(b=>b.KeySequenceAnalysis))
                //.Include(sess=>sess.KeySequences.Select(b=>b.KeySequenceAnalysis.Select(c=>c.AnalysisType)))
                .OrderByDescending(s => s.Created);

            return results.ToList();
        }
    }
}
