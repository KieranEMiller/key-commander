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
    public class HighlightedTextBuilderTests
    {
        public IList<HighlightDetail> run_determinator(string shown, string entered)
        {
            var accuracy = new Accuracy(new AnalyticData() { TextShown = shown, TextEntered = entered });
            accuracy.Compute();

            HighlightDeterminator det = new HighlightDeterminator();
            IList<HighlightDetail> details = det.Compute(accuracy);
            return details;
        }

        [Test]
        public void single_word_simple_incorrect_char_start_of_word()
        {
            var details = run_determinator("abc", "xbc");

            HighlightedTextBuilder builder = new HighlightedTextBuilder();
            Queue<HighlightedText> result = builder.Compute("abc", details);

            Assert.That(result, Is.EquivalentTo(
                new Queue<HighlightedText>(
                    new List<HighlightedText>(){
                        new HighlightedText() {
                            HighlightType = HighlightType.IncorrectChar,
                            Text = "a",
                        },
                        new HighlightedText() {
                            HighlightType = HighlightType.Normal,
                            Text="bc"
                        }
                    }
                )
            ));
        }

        [Test]
        public void single_word_simple_incorrect_char_middle_of_word()
        {
            var details = run_determinator("abc", "axc");

            HighlightedTextBuilder builder = new HighlightedTextBuilder();
            Queue<HighlightedText> result = builder.Compute("abc", details);

            Assert.That(result, Is.EquivalentTo(
                new Queue<HighlightedText>(
                    new List<HighlightedText>(){
                        new HighlightedText() {
                            HighlightType = HighlightType.Normal,
                            Text = "a",
                        },
                        new HighlightedText() {
                            HighlightType = HighlightType.IncorrectChar,
                            Text="b"
                        },
                        new HighlightedText() {
                            HighlightType = HighlightType.Normal,
                            Text="c"
                        }
                    }
                )
            ));
        }


    }
}
