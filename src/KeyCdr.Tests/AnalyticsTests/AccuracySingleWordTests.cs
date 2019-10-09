using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using KeyCdr.Analytics;

namespace KeyCdr.Tests.AnalyticsTests
{
    [Category("Analytics_Accuracy_SingleWord")]
    [TestFixture]
    public class AccuracySingleWordTests
    {
        [TestCase("qwer", "zxcv")]
        public void text_shown_and_entered_correct_after_compute(string input, string expected)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.GetTextShown(), Is.EqualTo(input));
            Assert.That(accuracy.GetTextEntered(), Is.EqualTo(expected));
        }

        [TestCase("a", "a", 0)]
        [TestCase("a", "z", 1)]
        [TestCase("abc", "abc", 0)]
        [TestCase("abc", "xyz", 3)]
        [TestCase("abc", "ayc", 1)]
        [TestCase("abc", "ayz", 2)]
        public void counts_number_of_wrong_chars_for_equal_length_strings(string input, string expected, int numDiffChars)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.NumIncorrectChars, Is.EqualTo(numDiffChars));
        }

        [TestCase("a", "a", 1)]
        [TestCase("a", "z", 0)]
        [TestCase("abc", "abc", 3)]
        [TestCase("abc", "xyz", 0)]
        [TestCase("abc", "ayc", 2)]
        [TestCase("abc", "zyc", 1)]
        public void counts_number_of_correct_chars_for_equal_length_strings(string input, string expected, int numSameChars)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.NumCorrectChars, Is.EqualTo(numSameChars));
        }

        [TestCase("qwer", "qw", 2)]
        [TestCase("qwer", "zx", 2)]
        [TestCase("qwer", "z", 3)]
        [TestCase("qwer", "", 4)]
        [TestCase("qwer", "qwer", 0)]
        [TestCase("qwer", "qwerqwer", 0)]
        public void counts_num_short_chars_when_expected_length_less_than_shown(string input, string expected, int numShortChars)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.NumShortChars, Is.EqualTo(numShortChars));
        }

        [TestCase("zxcv", "qw", 0)]
        [TestCase("qwer", "", 0)]
        [TestCase("qwer", "qwer", 0)]
        [TestCase("qwer", "qwerqwer", 4)]
        [TestCase("qwer", "qwerq", 1)]
        [TestCase("qwer", "zxcvq", 1)]
        [TestCase("qwer", "qwerqwer", 4)]
        public void counts_num_extra_chars_when_expected_length_greater_than_shown(string input, string expected, int numExtraChars)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.NumExtraChars, Is.EqualTo(numExtraChars));
        }

        [Test]
        public void is_calculated_to_configured_precision()
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = "qwerty", TextEntered = "qwer" });
            accuracy.Compute();

            double wrongExpected = (double)2 / 3;
            Assert.That(accuracy.AccuracyVal, Is.Not.EqualTo(wrongExpected));

            double expected = Math.Round((double)2 / 3, Constants.PRECISION_FOR_DECIMALS);
            Assert.That(accuracy.AccuracyVal, Is.EqualTo(expected));
        }

        [TestCase("abc", "xyz", 0.0)]
        [TestCase("abc", "abc", 1.0)]
        [TestCase("abcd", "qxcy", 0.25)]
        [TestCase("abcd", "zbxy", 0.25)]
        [TestCase("abcd", "zbcd", 0.75)]
        [TestCase("abcd", "abcz", 0.75)]
        public void calculated_correctly_for_equal_length_strings(string input, string expected, double expectedAcc)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.AccuracyVal, Is.EqualTo(expectedAcc), string.Format("{0} vs {1} = {2}", input, expected, expectedAcc));
        }

        [TestCase("abc", "xy", 0.0)]
        [TestCase("abcd", "qx", 0.0)]
        [TestCase("abcd", "azx", 0.25)]
        [TestCase("abcd", "zby", 0.25)]
        [TestCase("abcd", "a", 0.25)]
        public void calculated_correctly_for_shorted_strings(string input, string expected, double expectedAcc)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.AccuracyVal, Is.EqualTo(expectedAcc), string.Format("{0} vs {1} = {2}", input, expected, expectedAcc));
        }

        [TestCase("abc", "xyzy", 0.0)]
        [TestCase("abcd", "zxnvzxcv", 0.0)]
        [TestCase("abcd", "abcde", 0.80)]
        [TestCase("abcd", "abzdv", 0.60)]
        [TestCase("abcd", "abcdabcd", 0.50)]
        public void calculated_correctly_for_extra_long_strings(string input, string expected, double expectedAcc)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.AccuracyVal, Is.EqualTo(expectedAcc), string.Format("{0} vs {1} = {2}", input, expected, expectedAcc));
        }

        [TestCase("", "", 1.0)]
        [TestCase("abc", "", 0.0)]
        [TestCase("", "abc", 0.0)]
        public void edge_cases(string input, string expected, double expectedAcc)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.AccuracyVal, Is.EqualTo(expectedAcc), string.Format("{0} vs {1} = {2}", input, expected, expectedAcc));
        }

        [Test]
        public void summary_contains_accuracy_percent()
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = "abcd", TextEntered = "abxx" });
            accuracy.Compute();
            Assert.That(accuracy.GetResultSummary(), Contains.Substring("50%"));
        }
    }
}
