using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.TextSamples;
using NUnit.Framework;
using Moq;

namespace KeyCdr.Tests.TextSamplesTests
{
    [TestFixture]
    public class BaseTextSampleGeneratorTests
    {
        [Test]
        public void paragraph_block_ensure_plucked_text_is_trimmed()
        {
            string sampleText = "This is a test.  Multisentence paragraph.  With multiple sentences.  That have multiple lines.  And more than the min.  For a paragraph.  So that it.  Can be used to test.  The trimming logic.  ";

            Mock<BaseTextSampleGenerator> gen = new Mock<BaseTextSampleGenerator>();
            gen.Setup(g => g.GetRandomIndex(It.IsAny<int>())).Returns(2);

            string actual = gen.Object.GetParagraphFromTextBlock(sampleText);
            Assert.That(actual, Is.Not.StartsWith(" "));
            Assert.That(actual, Is.Not.EndsWith(" "));
        }

        [Test]
        public void extract_paragraph_plucks_out_expected_sentences_given_random_index()
        {
            string sampleText = "This is a test.  Multisentence paragraph.  With multiple sentences.  That have multiple lines.  And more than the min.  For a paragraph.  So that it.  Can be used to test.  The trimming logic. ";
            string expected = "With multiple sentences. That have multiple lines. And more than the min. For a paragraph.";

            Mock<BaseTextSampleGenerator> gen = new Mock<BaseTextSampleGenerator>();
            gen.Setup(g => g.GetRandomIndex(It.IsAny<int>())).Returns(2);

            string actual = gen.Object.GetParagraphFromTextBlock(sampleText);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void extract_paragraph_single_sentence()
        {
            string sampleText = "This is a test.";
            BaseTextSampleGenerator gen = new BaseTextSampleGenerator();
            string actual = gen.GetParagraphFromTextBlock(sampleText);

            Assert.That(actual, Is.EqualTo(sampleText));
        }

        [Test]
        public void extract_paragraph_sentence_lengths_up_to_max_equal_to_unaltered_input()
        {
            string SAMPLE_SENTENCE = "This is a test. ";
            StringBuilder paraBuilder = new StringBuilder();

            for(int i=0;i< BaseTextSampleGenerator.NUM_SENTENCES_IN_A_PARAGRAPH;i++)
            {
                paraBuilder.AppendFormat(SAMPLE_SENTENCE);
                string paragraphBlock = paraBuilder.ToString();

                BaseTextSampleGenerator gen = new BaseTextSampleGenerator();
                string actual = gen.GetParagraphFromTextBlock(paragraphBlock);

                Assert.That(actual, Is.EqualTo(paragraphBlock));
            }
        }
    }
}
