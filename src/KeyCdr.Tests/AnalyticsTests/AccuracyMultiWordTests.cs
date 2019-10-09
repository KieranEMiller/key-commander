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
        [TestCase("a c", "z q", 0)]
        [TestCase("aaa cccc eee", "z q eee", 3)]
        public void accuracy_mw_equal_numbers_of_words_base_cases(string shown, string entered, int numCorrectChars)
        {
            var accuracy = new Accuracy(new AnalyticData { TextShown = shown, TextEntered = entered });
            accuracy.Compute();
            Assert.That(accuracy.NumCorrectChars, Is.EqualTo(numCorrectChars), string.Format("{0} vs {1}", shown, entered));
        }

    }
}
