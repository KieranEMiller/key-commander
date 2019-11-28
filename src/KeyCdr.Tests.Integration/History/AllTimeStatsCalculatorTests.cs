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

namespace KeyCdr.Tests.Integration.History
{
    [TestFixture]
    public class AllTimeStatsCalculatorTests
    {
        private KCUser _user;

        [SetUp]
        public void TestSetup()
        {
            UserManager mgr = new UserManager();
            _user = mgr.CreateGuest();
        }

        [Test]
        public void all_time_stats_for_zero_previous_sessions_returns_zeros()
        {
            AllTimeStatsCalculator calc = new AllTimeStatsCalculator(_user);
            var speedCalcs = calc.GetSpeedAllTime();
            Assert.That(speedCalcs, Is.Null);

            var accuracyCalcs = calc.GetAccuracyAllTime();
            Assert.That(accuracyCalcs, Is.Null);
        }

        [Test]
        public void all_time_stats_for_one_previous_session_returns_just_that_session()
        {
            UserSession session = new UserSession(_user);
            ITextSample sample = new TextSample() {
                SourceKey = "http://some_url",
                SourceType = TextSampleSourceType.Wikipedia,
                Text = "this is some sample text shown"
            };

            KeySequence seq = session.StartNewSequence(sample);
            var results = session.StopSequence("text entered", TimeSpan.FromMilliseconds(500));

            AllTimeStatsCalculator calc = new AllTimeStatsCalculator(_user);

            var speedThisInstance = results
                .Where(r => r.GetAnalyticType() == Analytics.AnalyticType.Speed)
                .Cast<Analytics.Speed>()
                .FirstOrDefault();

            var speedCalcs = calc.GetSpeedAllTime();

            Assert.That(speedCalcs.TotalTimeInMilliSec, Is.EqualTo(speedThisInstance.TotalTime.TotalMilliseconds));
            Assert.That(speedCalcs.NumEntitiesRepresented, Is.EqualTo(1));
            Assert.That(speedCalcs.WordPerMin, Is.EqualTo(speedThisInstance.WordsPerMinute));
            Assert.That(speedCalcs.CharsPerSec, Is.EqualTo(speedThisInstance.CharsPerSecond));
        }

        [Test]
        public void all_time_stats_for_multiple_previous_sessions_averages()
        {
            UserSession session = new UserSession(_user);
            ITextSample sample = new TextSample() {
                SourceKey = "http://some_url",
                SourceType = TextSampleSourceType.Wikipedia,
                Text = "this is some sample text shown"
            };

            List<int> spans = new List<int>();
            for(int i=1; i <= 10;i++)
            {
                int timespan = i * 1000;
                spans.Add(timespan);

                KeySequence seq = session.StartNewSequence(sample);
                var results = session.StopSequence("text entered", TimeSpan.FromMilliseconds(timespan));
            }

            AllTimeStatsCalculator calc = new AllTimeStatsCalculator(_user);

            var speedCalcs = calc.GetSpeedAllTime();

            Assert.That(speedCalcs.TotalTimeInMilliSec, Is.EqualTo(spans.Average()));
            Assert.That(speedCalcs.NumEntitiesRepresented, Is.EqualTo(10));
        }
    }
}
