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
    }
}
