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
            var accuracy = new Accuracy(new AnalyticData() { TextShown = shown, TextEntered = entered });
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
            var accuracy = new Accuracy(new AnalyticData() { TextShown = shown, TextEntered = entered });
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

        [TestCase("abc xyz", "abc xyzw", 7, 8)]
        [TestCase("abc xyz", "abc xyzww", 7, 9)]
        [TestCase("abc xyz qwer", "abc xyz qwerqwerqwer", 12, 20)]
        [TestCase("abc xyz qwer", "abc xyzMM qwer", 7, 9)]
        [TestCase("abc xyz qwer", "abck xyz qwer", 3, 4)]
        public void multi_word_extra_chars(
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

        [TestCase("abc xyz", "abc xy", 6, 7)]
        [TestCase("abc xyz", "abc x", 5, 7)]
        [TestCase("abc xyz qwer", "abc xyz q", 9, 12)]
        [TestCase("abc xyz qwer", "abc xy qwer", 6, 7)]
        [TestCase("abc xyz qwer", "a xyz qwer", 1, 3)]
        public void multi_word_short_chars(
            string shown, string entered, int start, int end)
        {
            var accuracy = new Accuracy(new AnalyticData() { TextShown = shown, TextEntered = entered });
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

        [Test]
        public void complex_highlight_types_single_word_incorrect_and_extra()
        {
            var accuracy = new Accuracy(new AnalyticData() { TextShown = "qwer", TextEntered = "qqerxx" });
            accuracy.Compute();

            HighlightDeterminator det = new HighlightDeterminator();
            var details = det.Compute(accuracy);
            Assert.That(details.Count, Is.EqualTo(2));
            Assert.That(details, Is.EquivalentTo(new List<HighlightDetail>() {
                new HighlightDetail() {
                    HighlightType=HighlightType.IncorrectChar,
                    IndexStart=1,
                    IndexEnd=2,
                },
                new HighlightDetail() {
                    HighlightType=HighlightType.ExtraChars,
                    IndexStart=4,
                    IndexEnd=6,
                }
            }));
        }

        [Test]
        public void complex_highlight_types_single_word_incorrect_and_short()
        {
            var accuracy = new Accuracy(new AnalyticData() { TextShown = "qwer", TextEntered = "xwe" });
            accuracy.Compute();

            HighlightDeterminator det = new HighlightDeterminator();
            var details = det.Compute(accuracy);
            Assert.That(details.Count, Is.EqualTo(2));
            Assert.That(details, Is.EquivalentTo(new List<HighlightDetail>() {
                new HighlightDetail() {
                    HighlightType=HighlightType.IncorrectChar,
                    IndexStart=0,
                    IndexEnd=1,
                },
                new HighlightDetail() {
                    HighlightType=HighlightType.ShortChars,
                    IndexStart=3,
                    IndexEnd=4,
                }
            }));
        }

        [Test]
        public void complex_highlight_types_multi_word_case01()
        {
            var accuracy = new Accuracy(new AnalyticData() {
                TextShown = "abc qwer asdf", TextEntered = "abx qw asdfasdf"
            });
            accuracy.Compute();

            HighlightDeterminator det = new HighlightDeterminator();
            var details = det.Compute(accuracy);
            Assert.That(details.Count, Is.EqualTo(3));
            Assert.That(details, Is.EquivalentTo(new List<HighlightDetail>() {
                new HighlightDetail() {
                    HighlightType=HighlightType.IncorrectChar,
                    IndexStart=2,
                    IndexEnd=3,
                },
                new HighlightDetail() {
                    HighlightType=HighlightType.ShortChars,
                    IndexStart=6,
                    IndexEnd=8,
                },
                new HighlightDetail() {
                    HighlightType=HighlightType.ExtraChars,
                    IndexStart=13,
                    IndexEnd=17,
                }
            }));
        }
    }
}
