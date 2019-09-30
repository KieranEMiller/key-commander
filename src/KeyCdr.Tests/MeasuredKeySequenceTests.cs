using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Analytics;
using NUnit.Framework;

namespace KeyCdr.Tests
{
    [TestFixture]
    public class MeasuredKeySequenceTests
    {
        [Test]
        public void uses_a_default_analysis_type_when_none_set()
        {
            var seq = new MeasuredKeySequence(string.Empty);
            seq.Start();
            seq.Stop(string.Empty);

            Assert.That(seq.Analysis, Has.Count.GreaterThan(0));
        }

        [TestCase(AnalyticType.Speed)]
        [TestCase(AnalyticType.Accuracy)]
        public void uses_the_specific_analysis_type_when_one_set_in_ctor(AnalyticType type)
        {
            var seq = new MeasuredKeySequence(string.Empty, type);
            seq.Start();
            seq.Stop(string.Empty);
            Assert.That(seq.Analysis, Has.One.Matches<IAnalytic>(d => d.GetAnalyticType() == type));
        }

        [Test]
        public void uses_multiple_analysisttypes_when_used_in_ctor()
        {
            var seq = new MeasuredKeySequence(string.Empty, new List<AnalyticType> {AnalyticType.Accuracy, AnalyticType.Speed});
            seq.Start();
            seq.Stop(string.Empty);
            Assert.That(seq.Analysis, Has.One.Matches<IAnalytic>(d => d.GetAnalyticType() == AnalyticType.Accuracy));
            Assert.That(seq.Analysis, Has.One.Matches<IAnalytic>(d => d.GetAnalyticType() == AnalyticType.Speed));
        }

        [Test]
        public void running_generates_a_non_zero_time_span()
        {
            var seq = new MeasuredKeySequence(string.Empty, AnalyticType.Speed);
            seq.Start();
            seq.Stop(string.Empty);

            Assert.That(seq.Analysis, Has.One.Matches<Analytics.Speed>(a => a.TotalTime.Ticks > 0));
        }

        [Test]
        public void running_generates_a_ianalytic_with_all_run_data()
        {
            var seq = new MeasuredKeySequence("textin", AnalyticType.Accuracy);
            seq.Start();
            seq.Stop("textout");

            Assert.That(seq.Analysis, Has.One.Matches<Analytics.Accuracy>(a => a._analyticData.Elapsed.Ticks > 0));
            Assert.That(seq.Analysis, Has.One.Matches<Analytics.Accuracy>(a => a._analyticData.TextShown.Equals("textin")));
            Assert.That(seq.Analysis, Has.One.Matches<Analytics.Accuracy>(a => a._analyticData.TextEntered.Equals("textout")));
        }
    }
}
