using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using KeyCdr.Analytics;

namespace KeyCdr.Tests.AnalyticsTests
{
    [Category("analytics_speed")]
    [TestFixture]
    public class SpeedTests
    {
        [TestCase("a b c d", 60, 4)]
        [TestCase("a b c d", 30, 8)]
        [TestCase("a", 30, 2)]
        [TestCase("a", 60, 1)]
        [TestCase("the quick brown fox jumps over the lazy dog this_is_10_words", 10, 60)]
        [TestCase("", 10, 0)]
        public void given_timespan_in_s_wpm_is_accurate_for_whole_numbers(string text, int numSeconds, int expectedWpm)
        {
            var time = new TimeSpan(0, 0, 0, numSeconds);
            var data = new AnalyticData() { Elapsed = time, TextEntered = text};
            var speed = new Speed(data);
            speed.Compute();
            Assert.That(speed.WordsPerMinute, Is.EqualTo(expectedWpm), string.Format("text {0} in {1}s should be {2}", text, numSeconds, expectedWpm));
        }

        [TestCase("ab cd", 1, 5)]
        [TestCase("ab", 1, 2)]
        [TestCase("abcdefghij", 1, 10)]
        [TestCase("abcde", 5, 1)]
        public void given_timespan_in_s_chars_per_sec_accurate_for_whole_numbers(string text, int numSeconds, int expected)
        {
            var time = new TimeSpan(0, 0, 0, numSeconds);
            var data = new AnalyticData() { Elapsed = time, TextEntered = text};
            var speed = new Speed(data);
            speed.Compute();
            Assert.That(speed.CharsPerSecond, Is.EqualTo(expected), string.Format("text {0} in {1}s should be {2}", text, numSeconds, expected));
        }

        [Test]
        public void given_timespan_in_s_chars_per_sec_accurate_for_fractions()
        {
            var time = new TimeSpan(0, 0, 0, 6);
            var data = new AnalyticData() { Elapsed = time, TextEntered = "ab"};
            var speed = new Speed(data);
            speed.Compute();

            double notRoundedExpected = (double)2 / 6;
            Assert.That(speed.CharsPerSecond, Is.Not.EqualTo(notRoundedExpected));

            double roundedExpected = Math.Round((double)2 / 6, Constants.PRECISION_FOR_DECIMALS);
            Assert.That(speed.CharsPerSecond, Is.EqualTo(roundedExpected));
        }

        [TestCase(120, "abcd", 0.5)]
        [TestCase(10, "a b c d e", 30.0)]
        public void given_timespan_in_s_wpm_accurate_for_fractions(int numSeconds, string text, double expected)
        {
            var time = new TimeSpan(0, 0, 0, numSeconds);
            var data = new AnalyticData() { Elapsed = time, TextEntered = text};
            var speed = new Speed(data);
            speed.Compute();

            Assert.That(speed.WordsPerMinute, Is.EqualTo(expected), string.Format("text {0} in {1}s should be {2}", text, numSeconds, expected));
        }
    }
}
