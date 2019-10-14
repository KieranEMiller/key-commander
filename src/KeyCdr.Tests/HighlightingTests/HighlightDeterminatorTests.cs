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

        [TestCase("abc", "abz", 2, 3)]
        [TestCase("abc", "zbc", 0, 1)]
        [TestCase("abc", "azc", 1, 2)]
        public void one_word_one_incorrect_char_at_all_positions(
            string shown, string entered, int start, int end)
        {
            var accuracy = new Accuracy(new AnalyticData() { TextShown = shown, TextEntered = entered});
            accuracy.Compute();

            HighlightDeterminator det = new HighlightDeterminator();
            var details = det.Compute(accuracy);
            Assert.That(details.Count, Is.EqualTo(1));
            Assert.That(details, Is.EquivalentTo(new List<HighlightDetail>() {
                new HighlightDetail() {
                    HighlightType=HighlightType.IncorrectChar,
                    IndexStart=start,
                    IndexEnd=end,
                }
            }));
        }

        [TestCase("abc", "ab", 2, 3)]
        [TestCase("abc", "a", 1, 3)]
        public void one_word_one_short_chars(
            string shown, string entered, int start, int end)
        {
            var accuracy = new Accuracy(new AnalyticData() { TextShown = shown, TextEntered = entered});
            accuracy.Compute();

            HighlightDeterminator det = new HighlightDeterminator();
            var details = det.Compute(accuracy);
            Assert.That(details.Count, Is.EqualTo(1));
            Assert.That(details, Is.EquivalentTo(new List<HighlightDetail>() {
                new HighlightDetail() {
                    HighlightType=HighlightType.ShortChars,
                    IndexStart=start,
                    IndexEnd=end,
                }
            }));
        }

        [TestCase("abc", "abcd", 3, 4)]
        [TestCase("abc", "abcdefg", 3, 7)]
        public void one_word_one_extra_chars(
            string shown, string entered, int start, int end)
        {
            var accuracy = new Accuracy(new AnalyticData() { TextShown = shown, TextEntered = entered });
            accuracy.Compute();

            HighlightDeterminator det = new HighlightDeterminator();
            var details = det.Compute(accuracy);
            Assert.That(details.Count, Is.EqualTo(1));
            Assert.That(details, Is.EquivalentTo(new List<HighlightDetail>() {
                new HighlightDetail() {
                    HighlightType=HighlightType.ExtraChars,
                    IndexStart=start,
                    IndexEnd=end,
                }
            }));
        }
    }
}
