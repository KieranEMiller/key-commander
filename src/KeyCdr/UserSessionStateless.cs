using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;
using KeyCdr.TextSamples;
using KeyCdr.Analytics;
using KeyCdr.KeySequences;

namespace KeyCdr
{
    public class UserSessionStateless : UserSession
    {
        public UserSessionStateless()
            : base(null)
        { }

        public UserSessionStateless(KCUser user)
            : base(user, new WikipediaTextGenerator())
        { } 

        public UserSessionStateless(KCUser user, ITextSampleGenerator generator)
            : base(user, generator)
        { }

        public virtual KeySequence StartNewSequence(Session session, ITextSample sample)
        {
            _dbSeq = base.CreateNewSequence(session, sample);
            return _dbSeq;
        }

        public virtual IList<IAnalytic> Stop(
            IMeasuredKeySequence measuredSeq, Guid sequenceId, string entered)
        {
            IList<IAnalytic> results = measuredSeq.Stop(entered);
            KeySequence seq = new KeySequence();
            seq.KeySequenceId = sequenceId;
            seq.TextEntered = entered;

            foreach (var analytic in results)
                base.RecordAnalytic(seq, analytic);

            _db.SaveChanges();

            return results;
        }
    }
}
