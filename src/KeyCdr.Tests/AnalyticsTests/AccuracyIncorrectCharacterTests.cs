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
            Assert.That(incorrectChars.Count, Is.EqualTo(1));
            Assert.That(incorrectChars[0].Expected, Is.EqualTo('c'));
            Assert.That(incorrectChars[0].Found, Is.EqualTo('x'));
            Assert.That(incorrectChars[0].Index, Is.EqualTo(2));
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
            Assert.That(incorrectChars[0].Expected, Is.EqualTo('a'));
            Assert.That(incorrectChars[0].Found, Is.EqualTo('q'));
            Assert.That(incorrectChars[0].Index, Is.EqualTo(0));

            Assert.That(incorrectChars[1].Expected, Is.EqualTo('c'));
            Assert.That(incorrectChars[1].Found, Is.EqualTo('x'));
            Assert.That(incorrectChars[1].Index, Is.EqualTo(2));
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
            Assert.That(incorrectChars[0].Expected, Is.EqualTo('a'));
            Assert.That(incorrectChars[0].Found, Is.EqualTo('x'));
            Assert.That(incorrectChars[0].Index, Is.EqualTo(0));

            Assert.That(incorrectChars[1].Expected, Is.EqualTo('b'));
            Assert.That(incorrectChars[1].Found, Is.EqualTo('y'));
            Assert.That(incorrectChars[1].Index, Is.EqualTo(1));

            Assert.That(incorrectChars[2].Expected, Is.EqualTo('c'));
            Assert.That(incorrectChars[2].Found, Is.EqualTo('z'));
            Assert.That(incorrectChars[2].Index, Is.EqualTo(2));
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
            Assert.That(incorrectChars[0].Expected, Is.EqualTo('z'));
            Assert.That(incorrectChars[0].Found, Is.EqualTo('y'));
            Assert.That(incorrectChars[0].Index, Is.EqualTo(0));
        }
    }
}
