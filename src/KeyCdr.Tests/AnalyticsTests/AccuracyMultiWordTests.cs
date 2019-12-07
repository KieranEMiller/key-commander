using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Analytics;
using NUnit.Framework;

namespace KeyCdr.Tests.AnalyticsTests
{
    [Category("Analytics_Accuracy_MultiWord")]
    [TestFixture]
    public class AccuracyMultiWordTests
    {
        [TestCase("ab cd g", "ab cd g", 5)]
        [TestCase("ab cd g", "ab xd g", 4)]
        [TestCase("ab cd g", "ab xz g", 3)]
        [TestCase("ab cd g", "as xdm g", 3)]
        [TestCase("aaa cccc eee", "z q eee", 3)]
        public void multi_word_correct_chars_base_cases(string shown, string entered, int numCorrectChars)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = shown, TextEntered = entered });
            accuracy.Compute();
            Assert.That(accuracy.NumCorrectChars, Is.EqualTo(numCorrectChars), string.Format("{0} vs {1}", shown, entered));
        }

        [TestCase("a c", "z q", 0)]
        [TestCase("a c", "a c", 2)]
        [TestCase("", "", 0)]
        public void multi_word_correct_chars_edge_cases(string shown, string entered, int numCorrectChars)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = shown, TextEntered = entered });
            accuracy.Compute();
            Assert.That(accuracy.NumCorrectChars, Is.EqualTo(numCorrectChars), string.Format("{0} vs {1}", shown, entered));
        }

        [TestCase("a c", "a c", 0)]
        [TestCase("aaa cc", "aab cc", 1)]
        [TestCase("aaa cc", "aab cd", 2)]
        [TestCase("aaa cc", "xxx zz", 5)]
        public void multi_word_incorrect_chars_base_cases(string shown, string entered, int numIncorrectChars)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = shown, TextEntered = entered });
            accuracy.Compute();
            Assert.That(accuracy.NumIncorrectChars, Is.EqualTo(numIncorrectChars), string.Format("{0} vs {1}", shown, entered));
        }

        [Test]
        public void compute_accuracy_with_no_measurements_returns_zero_does_not_throw_exception()
        {
            var accuracy = new Accuracy();
            double val = -1;
            Assert.DoesNotThrow(()=>val = accuracy.AccuracyVal);
        }

        [Test]
        public void char_index_accounts_for_spaces_two_words()
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = "abc xyz", TextEntered = "abc xyz" });
            accuracy.Compute();
            Assert.That(accuracy.Measurements.Count, Is.EqualTo(2));
            Assert.That(accuracy.Measurements[0].IndexInLargerText, Is.EqualTo(0));
            Assert.That(accuracy.Measurements[1].IndexInLargerText, Is.EqualTo(4));
        }

        [Test]
        public void char_index_accounts_for_spaces_4_words()
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = "abc xyz qwer asdf", TextEntered = "abc xyz qwer asdf" });
            accuracy.Compute();
            Assert.That(accuracy.Measurements.Count, Is.EqualTo(4));
            Assert.That(accuracy.Measurements[0].IndexInLargerText, Is.EqualTo(0));
            Assert.That(accuracy.Measurements[1].IndexInLargerText, Is.EqualTo(4));
            Assert.That(accuracy.Measurements[2].IndexInLargerText, Is.EqualTo(8));
            Assert.That(accuracy.Measurements[3].IndexInLargerText, Is.EqualTo(13));
        }

        [Test]
        public void double_spaces_between_words_doesnt_affect_accuracy()
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = "abc xyz", TextEntered = "abc  xyz" });
            accuracy.Compute();
            Assert.That(accuracy.NumIncorrectChars, Is.EqualTo(0));
            Assert.That(accuracy.TotalNumWordsShown, Is.EqualTo(accuracy.TotalNumWordsEntered));
            Assert.That(accuracy.NumShortChars, Is.EqualTo(0));
            Assert.That(accuracy.NumExtraChars, Is.EqualTo(0));
        }
    }
}
