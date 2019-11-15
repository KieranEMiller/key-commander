using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;
using KeyCdr.TextSamples;
using KeyCdr.Analytics;

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

        private ITextSampleGenerator _generator;

        private MeasuredKeySequence _currentSeq;

        private Data.KCUser _user;
        private Data.Session _session;
        private Data.KeySequence _dbSeq;

        public void NewSession()
        {
            _session = new Session();
            _session.SessionId = Guid.NewGuid();
            _session.UserId = _user.UserId;
            _session.Created = DateTime.Now;
            _db.Session.Add(_session);
        }

        public void StartNewSequence(ITextSample sample)
        {
            if (_session == null) NewSession();

            _dbSeq = new KeySequence();
            _dbSeq.KeySequenceId = Guid.NewGuid();
            _dbSeq.Session = _session;
            _dbSeq.SourceKey = sample.GetSourceKey();
            _dbSeq.TextShown = sample.GetText();
            _dbSeq.SourceTypeId = (int) sample.GetSourceType();
            _dbSeq.Created = System.DateTime.Now;
            _dbSeq.TextEntered = null;

            _db.KeySequence.Add(_dbSeq);
            _db.SaveChanges();

            _currentSeq = new MeasuredKeySequence(sample.GetText());
            _currentSeq.Start();
        }

        public IList<IAnalytic> Peek(string entered)
        {
            return _currentSeq.Peek(entered);
        }

        public IList<IAnalytic> Stop(string entered)
        {
            IList<IAnalytic> results = _currentSeq.Stop(entered);
            _dbSeq.TextEntered = entered;

            foreach(var analytic in results)
                RecordAnalytic(analytic);

            _db.SaveChanges();

            return results;
        }

        public void RecordAnalytic(IAnalytic analytic)
        {
            Data.KeySequenceAnalysis analysis = new Data.KeySequenceAnalysis();
            analysis.KeySequenceAnalysisId = Guid.NewGuid();
            analysis.KeySequenceId = _dbSeq.KeySequenceId;
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
