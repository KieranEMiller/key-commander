using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;
using KeyCdr.History;
using KeyCdr.TextSamples;
using KeyCdr.Users;
using NUnit.Framework;

namespace KeyCdr.Tests.Integration.Session
{
    [TestFixture]
    public class UserSessionHistoryTests : BaseIntegrationTest
    {
        private KCUser _user;

        [SetUp]
        public void TestSetup()
        {
            UserManager mgr = new UserManager();
            _user = mgr.CreateGuest();
        }

        [Test]
        public void starting_new_sequence_without_explicitly_creating_new_session_creates_one_automatically()
        {
            UserSession session = new UserSession(_user);
            ITextSample sample = new TextSample() {
                SourceKey = "http://some_url",
                SourceType = TextSampleSourceType.Wikipedia,
                Text = "this is some sample text"
            };

            KeySequence seq = session.StartNewSequence(sample);

            UserSessionHistory history = new UserSessionHistory();
            var sessions = history.GetSessionsByUser(_user);
            Assert.That(sessions, Is.Not.Null);
            Assert.That(sessions.Count, Is.EqualTo(1));
        }

        [Test]
        public void stopping_sequence_saves_entered_text()
        {
            UserSession session = new UserSession(_user);
            ITextSample sample = new TextSample()
            {
                SourceKey = "http://some_url",
                SourceType = TextSampleSourceType.Wikipedia,
                Text = "this is some sample text shown"
            };

            KeySequence seq = session.StartNewSequence(sample);
            session.StopSequence("text entered");

            UserSessionHistory history = new UserSessionHistory();
            var keySeq = history.GetHistoryDetailsByKeySequence(seq.KeySequenceId);

            Assert.That(keySeq, Is.Not.Null);
            Assert.That(keySeq.TextEntered, Is.EqualTo("text entered"));
        }
    }
}
