using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Analytics;
using NUnit.Framework;

namespace KeyCdr.Tests.AnalyticsTests
{
    [TestFixture]
    public class AccuracyIncorrectCharacterTests
    {
        [Test]
        public void incorrect_char_list_is_empty_for_equal_strings()
        {
            string input = "abcd";
            string expected = "abcd";
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();
            Assert.That(accuracy.GetAllIncorrectCharacters(), Is.Empty);
        }

        [Test]
        public void one_word_one_wrong_char()
        {
            string input = "abcd";
            string expected = "abxd";
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();

            var incorrectChars = accuracy.GetAllIncorrectCharacters();
            Assert.That(incorrectChars, Is.EquivalentTo(new List<AccuracyIncorrectChar>() {
                new AccuracyIncorrectChar() {
                    Expected='c',
                    Found='x',
                    Index=2
                }
            }));
            Assert.That(incorrectChars.Count, Is.EqualTo(1));
        }

        [Test]
        public void one_word_multiple_wrong_chars()
        {
            string input = "abcd";
            string expected = "qbxd";
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();

            var incorrectChars = accuracy.GetAllIncorrectCharacters();
            Assert.That(incorrectChars.Count, Is.EqualTo(2));
            Assert.That(incorrectChars, Is.EquivalentTo(new List<AccuracyIncorrectChar>() {
                new AccuracyIncorrectChar() {
                    Expected='a',
                    Found='q',
                    Index=0
                },
                new AccuracyIncorrectChar() {
                    Expected='c',
                    Found='x',
                    Index=2
                }
            }));
        }

        [Test]
        public void one_word_all_wrong_chars()
        {
            string input = "abc";
            string expected = "xyz";
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();

            var incorrectChars = accuracy.GetAllIncorrectCharacters();
            Assert.That(incorrectChars.Count, Is.EqualTo(3));
            Assert.That(incorrectChars, Is.EquivalentTo(new List<AccuracyIncorrectChar>() {
                new AccuracyIncorrectChar() {
                    Expected='a',
                    Found='x',
                    Index=0
                },
                new AccuracyIncorrectChar() {
                    Expected='b',
                    Found='y',
                    Index=1
                },
                new AccuracyIncorrectChar() {
                    Expected='c',
                    Found='z',
                    Index=2
                }
            }));
        }

        [Test]
        public void multi_word_one_wrong_word()
        {
            string input = "abc zy";
            string expected = "abc yy";
            var accuracy = new Accuracy(new AnalyticData { TextShown = input, TextEntered = expected });
            accuracy.Compute();

            var measurements = accuracy.Measurements;
            var incorrectChars = measurements.SelectMany(m=>m.IncorrectChars).ToList();

            Assert.That(incorrectChars.Count, Is.EqualTo(1));
            Assert.That(incorrectChars, Is.EquivalentTo(new List<AccuracyIncorrectChar>() {
                new AccuracyIncorrectChar() {
                    Expected='z',
                    Found='y',
                    Index=0
                }
            }));
        }

        [Test]
        public void equality_fails_when_null()
        {
            AccuracyIncorrectChar obj1 = new AccuracyIncorrectChar()
            {
                Index=2,
                Found='a',
                Expected ='b'
            };
            AccuracyIncorrectChar obj2 = null;
            Assert.That(obj1, Is.Not.EqualTo(obj2));
        }

        [Test]
        public void equality_fails_when_wrong_obj_type()
        {
            AccuracyIncorrectChar obj1 = new AccuracyIncorrectChar() {
                Index=2,
                Found='a',
                Expected ='b'
            };
            string obj2 = null;
            Assert.That(obj1, Is.Not.EqualTo(obj2));
        }

        [Test]
        public void equality_fails_when_one_property_incorrect()
        {
            AccuracyIncorrectChar obj1 = new AccuracyIncorrectChar() {
                Index=2,
                Found='a',
                Expected ='b'
            };
            AccuracyIncorrectChar obj2 = new AccuracyIncorrectChar() {
                Index=2,
                Found='a',
                Expected ='x'
            };
            Assert.That(obj1, Is.Not.EqualTo(obj2));
        }

        [Test]
        public void equality_true_when_all_properties_same()
        {
            AccuracyIncorrectChar obj1 = new AccuracyIncorrectChar() {
                Index=2,
                Found='a',
                Expected ='x'
            };
            AccuracyIncorrectChar obj2 = new AccuracyIncorrectChar() {
                Index=2,
                Found='a',
                Expected ='x'
            };
            Assert.That(obj1, Is.EqualTo(obj2));
        }
    }
}
