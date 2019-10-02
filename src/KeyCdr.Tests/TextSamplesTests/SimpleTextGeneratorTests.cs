using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using KeyCdr.TextSamples;

namespace KeyCdr.Tests.TextSamplesTests
{
    [TestFixture]
    public class SimpleTextGeneratorTests
    {
        [TestCase("qwer", new string[]{"qwer"})]
        [TestCase("qwer asdf", new string[]{"qwer", "asdf"})]
        [TestCase("qwer asdf zxcv", new string[]{"qwer", "asdf", "zxcv"})]
        public void simple_word_parsed_correctly(string input, string[] possibleMatches)
        {
            var simple = new SimpleTextGenerator(input);
            Assert.That(simple.GetWord().GetText(), Is.AnyOf(possibleMatches));
        }

        [TestCase("qwer      ", new string[]{"qwer"})]
        [TestCase("qwer     asdf     ", new string[]{"qwer", "asdf"})]
        [TestCase("     qwer asdf   zxcv", new string[]{"qwer", "asdf", "zxcv"})]
        public void simple_word_misc_whitespace_chars_get_stripped(string input, string[] possibleMatches)
        {
            var simple = new SimpleTextGenerator(input);
            Assert.That(simple.GetWord().GetText(), Is.AnyOf(possibleMatches));
        }

        [Test]
        [Repeat(3)]
        public void simple_word_tabs_get_stripped()
        {
            var stringb = new StringBuilder();
            stringb.Append("  qwer");
            stringb.Append("\t\t");
            stringb.Append("  ");
            stringb.Append("zxcv\t");
            var simple = new SimpleTextGenerator(stringb.ToString());
            Assert.That(simple.GetWord().GetText(), Is.AnyOf(new string[] { "qwer", "zxcv" }));
        }

        [Test]
        [Repeat(3)]
        public void simple_word_tabs_newlines_get_stripped()
        {
            var stringb = new StringBuilder();
            stringb.Append("  qwer   ");
            stringb.Append(Environment.NewLine);
            stringb.Append(Environment.NewLine);
            stringb.Append("zxcv\t");
            stringb.Append(Environment.NewLine);
            var simple = new SimpleTextGenerator(stringb.ToString());
            Assert.That(simple.GetWord().GetText(), Is.AnyOf(new string[] { "qwer", "zxcv" }));
        }

        [TestCase("This is a test-it contains a few sent; With diff blah blah. Yada Yada Yada" , new string[]
            {"This is a test-it contains a few sent", "With diff blah blah", "Yada Yada Yada" })]
        public void simple_sentence_parsed_correctly(string fullText, string[] possibilities)
        {
            var simple = new SimpleTextGenerator(fullText);
            Assert.That(simple.GetSentence().GetText(), Is.AnyOf(possibilities)); 
        }

        [TestCase("  This is         a test-it   contains      a   few sent; With  diff blah blah    . Yada Yada Yada    ", new string[]
            {"This is a test-it contains a few sent", "With diff blah blah", "Yada Yada Yada" })]
        public void simple_sentence_extra_whitespace(string fullText, string[] possibilities)
        {
            var simple = new SimpleTextGenerator(fullText);
            Assert.That(simple.GetSentence().GetText(), Is.AnyOf(possibilities));
        }

        [Test]
        [Repeat(3)]
        public void simple_sentence_tabs_newlines_get_stripped()
        {
            var stringb = new StringBuilder();
            stringb.Append("This is a test sentence.  Sentence Two. \t\t");
            stringb.Append(Environment.NewLine);
            stringb.Append(Environment.NewLine);
            stringb.Append("THis is another; and a 4th\t");
            stringb.Append(Environment.NewLine);
            var simple = new SimpleTextGenerator(stringb.ToString());
            Assert.That(simple.GetSentence().GetText(), Is.AnyOf(
                new string[] {
                    "This is a test sentence", "Sentence Two", "THis is another", "and a 4th"
                }
            ));
        }

        [Test]
        public void simple_paragraph_few_sentences_returns_everything()
        {
            var stringb = new StringBuilder();
            for(int i=0; i< TextSamples.SimpleTextGenerator.NUM_SENTENCES_IN_A_PARAGRAPH-1;i++)
            {
                stringb.AppendFormat("This is a test sentence - {0}. ", i.ToString());
            }

            var simple = new SimpleTextGenerator(stringb.ToString());
            Assert.That(simple.GetParagraph().GetText(), Is.EqualTo(stringb.ToString().Trim()));
        }

        [Test]
        [Repeat(10)]
        public void simple_paragraph_complex_paragraph()
        {
            List<string> allSentences = new List<string>();
            var stringb = new StringBuilder();
            for(int i=0; i < 10;i++)
            {
                string sample = string.Format("This is a test sentence - {0}. ", i.ToString());
                stringb.Append(sample);
                allSentences.Add(sample);
            }

            List<string> possibilities = new List<string>();
            for (int i = 0; i <= allSentences.Count - SimpleTextGenerator.NUM_SENTENCES_IN_A_PARAGRAPH; i++)
            {
                possibilities.Add(string.Join("", allSentences.Skip(i).Take(SimpleTextGenerator.NUM_SENTENCES_IN_A_PARAGRAPH)).Trim());
            }

            var simple = new SimpleTextGenerator(stringb.ToString());
            string paragraph = simple.GetParagraph().GetText();
            Assert.That(paragraph, Is.AnyOf(possibilities.ToArray()));
        }
    }
}
