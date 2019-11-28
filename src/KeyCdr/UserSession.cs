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
    public class UserSession : BaseDBAccess
    {
        public UserSession()
            : this(null)
        { }

        public UserSession(KCUser user)
            : this(user, new WikipediaTextGenerator())
        { } 

        public UserSession(KCUser user, ITextSampleGenerator generator)
        {
            _user = user;
            _generator = generator;
        }

        protected ITextSampleGenerator _generator;
        protected IMeasuredKeySequence _currentSeq;

        protected KCUser _user;
        protected Session _session;
        protected KeySequence _dbSeq;

        public virtual Session StartNewSession()
        {
            _session = new Session();
            _session.SessionId = Guid.NewGuid();
            _session.UserId = _user.UserId;
            _session.Created = DateTime.Now;

            _db.Session.Add(_session);

            return _session;
        }

        public KeySequence CreateNewSequence(Session session, ITextSample sample)
        {
            var seq  = new KeySequence();
            seq.KeySequenceId = Guid.NewGuid();
            seq.Session = session;
            seq.SourceKey = sample.GetSourceKey();
            seq.TextShown = sample.GetText();
            seq.SourceTypeId = (int)sample.GetSourceType();
            seq.Created = System.DateTime.Now;
            seq.TextEntered = null;

            _db.KeySequence.Add(seq);
            _db.SaveChanges();

            return seq;
        }

        public virtual KeySequence StartNewSequence(ITextSample sample)
        {
            if (_session == null) StartNewSession();

            _dbSeq = CreateNewSequence(_session, sample);

            _currentSeq = new MeasuredKeySequence();
            _currentSeq.Start(sample.GetText());

            return _dbSeq;
        }

        public virtual IList<IAnalytic> StopSequence(string entered)
        {
            return this.StopSequence(entered, null);
        }

        public virtual IList<IAnalytic> StopSequence(string entered, TimeSpan? elapsed)
        {
            IList<IAnalytic> results;
            if (!elapsed.HasValue)
                results = _currentSeq.Stop(entered);
            else
                results = _currentSeq.Stop(entered, elapsed.Value);
           
            _dbSeq.TextEntered = entered;

            foreach(var analytic in results)
                RecordAnalytic(_dbSeq, analytic);

            _db.SaveChanges();

            return results;
        }

        public virtual IList<IAnalytic> Peek(string entered)
        {
            return _currentSeq.Peek(entered);
        }

        public void RecordAnalytic(KeySequence seq, IAnalytic analytic)
        {
            KeySequenceAnalysis analysis = new KeySequenceAnalysis();
            analysis.KeySequenceAnalysisId = Guid.NewGuid();
            analysis.KeySequenceId = seq.KeySequenceId;
            analysis.Created = DateTime.Now;
            analysis.AnalysisTypeId = (int)analytic.GetAnalyticType();
            _db.KeySequenceAnalysis.Add(analysis);

            if (analytic is IAnalyticRecordable)
                ((IAnalyticRecordable)analytic).Record(_db, analysis);
        }

        public ITextSample GetWord()
        { return _generator.GetWord(); }

        public ITextSample GetSentence()
        { return _generator.GetSentence(); }

        public ITextSample GetParagraph()
        { return _generator.GetParagraph(); }
    }
}
