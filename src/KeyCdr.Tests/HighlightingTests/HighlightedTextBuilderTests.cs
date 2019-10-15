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

        [Test]
        public void single_word_simple_incorrect_char_end_of_word()
        {
            var details = run_determinator("abc", "abx");

            HighlightedTextBuilder builder = new HighlightedTextBuilder();
            Queue<HighlightedText> result = builder.Compute("abc", details);

            Assert.That(result, Is.EquivalentTo(
                new Queue<HighlightedText>(
                    new List<HighlightedText>(){
                        new HighlightedText() {
                            HighlightType = HighlightType.Normal,
                            Text = "ab",
                        },
                        new HighlightedText() {
                            HighlightType = HighlightType.IncorrectChar,
                            Text="c"
                        },
                    }
                )
            ));
        }

        [Test]
        public void single_word_multiple_incorrect_chars_end_of_text()
        {
            var details = run_determinator("abc", "axx");

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
                            HighlightType = HighlightType.IncorrectChar,
                            Text="c"
                        },
                    }
                )
            ));
        }

        [Test]
        public void single_word_multiple_incorrect_chars_start_of_text()
        {
            var details = run_determinator("abc", "xxc");

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
                            HighlightType = HighlightType.IncorrectChar,
                            Text="b"
                        },
                        new HighlightedText() {
                            HighlightType = HighlightType.Normal,
                            Text="c"
                        },
                    }
                )
            ));
        }

        [Test]
        public void multi_word_single_incorrect_chars_start_of_text_firstword()
        {
            var details = run_determinator("abc xyz", "wbc xyz");

            HighlightedTextBuilder builder = new HighlightedTextBuilder();
            Queue<HighlightedText> result = builder.Compute("abc xyz", details);

            Assert.That(result, Is.EquivalentTo(
                new Queue<HighlightedText>(
                    new List<HighlightedText>(){
                        new HighlightedText() {
                            HighlightType = HighlightType.IncorrectChar,
                            Text = "a",
                        },
                        new HighlightedText() {
                            HighlightType = HighlightType.Normal,
                            Text="bc xyz"
                        },
                    }
                )
            ));
        }

        [Test]
        public void multi_word_single_incorrect_chars_start_of_text_secondword()
        {
            var details = run_determinator("abc xyz", "abc wyz");

            HighlightedTextBuilder builder = new HighlightedTextBuilder();
            Queue<HighlightedText> result = builder.Compute("abc xyz", details);

            Assert.That(result, Is.EquivalentTo(
                new Queue<HighlightedText>(
                    new List<HighlightedText>(){
                        new HighlightedText() {
                            HighlightType = HighlightType.Normal,
                            Text = "abc ",
                        },
                        new HighlightedText() {
                            HighlightType = HighlightType.IncorrectChar,
                            Text="x"
                        },
                        new HighlightedText() {
                            HighlightType = HighlightType.Normal,
                            Text="yz"
                        },
                    }
                )
            ));
        }

        [Test]
        public void large_gap_in_incorrect_sections_does_not_throw_exception_for_out_of_bounds()
        {
            string orig = "Brett Rodwell (born 23 May 1970) is an Australian former professional rugby league footballer who played in the 1980s and 1990s";
            var details = run_determinator(
                orig,
                "Brett Rodwell (born 23 May 197y0) is an Australian former professional ruby league footballer who played in the 190s and 1990s"
            );

            HighlightedTextBuilder builder = new HighlightedTextBuilder();
            Assert.DoesNotThrow(() => builder.Compute(orig, details));
        }
    }
}
