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
            session.StopSequence("text entered", TimeSpan.FromMilliseconds(500));

            UserSessionHistory history = new UserSessionHistory();
            var keySeq = history.GetHistoryDetailsByKeySequence(seq.KeySequenceId);

            Assert.That(keySeq, Is.Not.Null);
            Assert.That(keySeq.TextEntered, Is.EqualTo("text entered"));
        }


        [Test]
        public void stopping_sequence_saves_default_analytics()
        {
            UserSession session = new UserSession(_user);
            ITextSample sample = new TextSample()
            {
                SourceKey = "http://some_url",
                SourceType = TextSampleSourceType.Wikipedia,
                Text = "this is some sample text shown"
            };

            KeySequence seq = session.StartNewSequence(sample);
            session.StopSequence("text entered", TimeSpan.FromSeconds(1));

            UserSessionHistory history = new UserSessionHistory();
            var keySeq = history.GetHistoryDetailsByKeySequence(seq.KeySequenceId);

            var speed = keySeq.KeySequenceAnalysis
                .Where(ks => ks.AnalysisTypeId == (int)Analytics.AnalyticType.Speed)
                .Select(s=>s.AnalysisSpeed)
                .FirstOrDefault();
            Assert.That(speed, Is.Not.Null);

            var accuracy = keySeq.KeySequenceAnalysis
                .Where(ks=>ks.AnalysisTypeId == (int) Analytics.AnalyticType.Accuracy)
                .Select(s=>s.AnalysisAccuracy)
                .FirstOrDefault();
            Assert.That(accuracy, Is.Not.Null);
        }

        [Test]
        public void stateless_stopping_sequence_saves_speed_with_correct_values()
        {
            UserSessionStateless session = new UserSessionStateless(_user);
            ITextSample sample = new TextSample()
            {
                SourceKey = "http://some_url",
                SourceType = TextSampleSourceType.Wikipedia,
                Text = "this is some sample text shown"
            };

            string enteredText = "this is some SAMple tex shownn";
            int elapsedInSec = 5;
            KeySequence seq = session.StartNewSequence(sample);
            session.StopSequence(seq.KeySequenceId, enteredText, TimeSpan.FromSeconds(elapsedInSec));

            UserSessionHistory history = new UserSessionHistory();
            var keySeq = history.GetHistoryDetailsByKeySequence(seq.KeySequenceId);

            var speed = keySeq.KeySequenceAnalysis
                .Where(ks => ks.AnalysisTypeId == (int)Analytics.AnalyticType.Speed)
                .Select(s=>s.AnalysisSpeed)
                .FirstOrDefault();

            Assert.That(speed, Is.Not.Null);
            Assert.That(speed.TotalTimeInMilliSec, Is.EqualTo(elapsedInSec*1000));

            double charsPerSec = (double)enteredText.Length / (double)elapsedInSec;
            Assert.That(speed.CharsPerSec, Is.EqualTo(Math.Round(charsPerSec, 2)));

            int numWords = enteredText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            double wordsPerMin = (double)numWords / (elapsedInSec / (double)60);
            Assert.That(speed.WordPerMin, Is.EqualTo(wordsPerMin));
        }

        [Test]
        public void stateless_stopping_sequence_saves_accuracy_with_correct_values()
        {
            UserSessionStateless session = new UserSessionStateless(_user);
            ITextSample sample = new TextSample()
            {
                SourceKey = "http://some_url",
                SourceType = TextSampleSourceType.Wikipedia,
                Text = "this is some sample text shown"
            };

            string enteredText = "this is somee SAMple tex shownn";
            int elapsedInSec = 5;
            KeySequence seq = session.StartNewSequence(sample);
            session.StopSequence(seq.KeySequenceId, enteredText, TimeSpan.FromSeconds(elapsedInSec));

            UserSessionHistory history = new UserSessionHistory();
            var keySeq = history.GetHistoryDetailsByKeySequence(seq.KeySequenceId);

            var accuracy = keySeq.KeySequenceAnalysis
                .Where(ks => ks.AnalysisTypeId == (int)Analytics.AnalyticType.Accuracy)
                .Select(s=>s.AnalysisAccuracy)
                .FirstOrDefault();

            Assert.That(accuracy, Is.Not.Null);
            Assert.That(accuracy.NumIncorrectChars, Is.EqualTo(3));
            Assert.That(accuracy.NumExtraChars, Is.EqualTo(2));
            Assert.That(accuracy.NumShortChars, Is.EqualTo(1));
            Assert.That(accuracy.NumWords, Is.EqualTo(6));

            //TODO: address this...
            //spaces are not currently counted towards the total number of characters
            //should they be?!...
            //Assert.That(accuracy.NumChars, Is.EqualTo(sample.GetText().Length));
            //Assert.That(accuracy.NumCorrectChars, Is.EqualTo(sample.GetText().Length - accuracy.NumIncorrectChars));
        }
    }
}
