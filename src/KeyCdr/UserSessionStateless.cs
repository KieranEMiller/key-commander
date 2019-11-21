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

        public KeySequence GetById(Guid seqId)
        {
            return _db.KeySequence
                .Where(ks => ks.KeySequenceId.Equals(seqId))
                .FirstOrDefault();
        }

        public virtual IList<IAnalytic> Stop(Guid seqId, string textEntered, TimeSpan timespan)
        {
            KeySequence keySeq = GetById(seqId);
            keySeq.TextEntered = textEntered;

            IMeasuredKeySequence measuredSequence = new MeasuredKeySequence();
            measuredSequence.Start(keySeq.TextShown);

            IList<IAnalytic> results = measuredSequence.Stop(keySeq.TextEntered, timespan);

            foreach (var analytic in results)
                base.RecordAnalytic(keySeq, analytic);

            _db.SaveChanges();

            return results;
        }
    }
}
