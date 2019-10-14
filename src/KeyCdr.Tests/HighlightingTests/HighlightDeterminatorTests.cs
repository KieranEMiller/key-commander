using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Analytics;
using KeyCdr.Highlighting;
using NUnit.Framework;

namespace KeyCdr.Tests.HighlightingTests
{
    [TestFixture]
    public class HighlightDeterminatorTests
    {
        [Test]
        public void empty_measurement_returns_no_highlights_no_exception()
        {
            var accuracy = new Accuracy();
            accuracy.Measurements = new List<AccuracyMeasurement>() { };

            HighlightDeterminator det = new HighlightDeterminator();
            Assert.DoesNotThrow(() => det.Compute(accuracy));
            Assert.That(det.Compute(accuracy), Is.Empty);
        }

        [Test]
        public void one_word_one_incorrect_char()
        {
            var accuracy = new Accuracy(new AnalyticData() { TextShown = "abc", TextEntered = "abq" });
            accuracy.Compute();

            HighlightDeterminator det = new HighlightDeterminator();
            var details = det.Compute(accuracy);
            
        }
    }
}
