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
            string sampleText = "This is a test.  Multisentence paragraph.  With multiple sentences.  That have multiple lines.  And more than the min.  For a paragraph.  So that it.  Can be used to test.  The trimming logic. "; 
            Mock<BaseTextSampleGenerator> gen = new Mock<BaseTextSampleGenerator>();
            gen.Setup(g => g.GetRandomIndex(It.IsAny<int>())).Returns(2);

            string actual = gen.Object.GetParagraphFromTextBlock(sampleText);
            Assert.That(actual, Is.Not.StartsWith(" "));
        }
    }
}
