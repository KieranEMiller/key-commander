using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using KeyCdr.TextSamples;
using Moq;
using AngleSharp;
using AngleSharp.Dom;
namespace KeyCdr.Tests.TextSamplesTests
{
    //the tests here are incomplete and don't do a good job of fully 
    //exercising the WikipediaTextGenerator class but I have been unsuccesful getting around
    //the fact that the OpenAsync in AngleSharp is an extension method!  
    //why did they do this?!
    [TestFixture]
    public class WikipediaTextGeneratorTests
    {
        private WikipediaTextGenerator _wikiGenerator;

        [SetUp]
        public void TestSetup()
        {
            var config = new Moq.Mock<IConfiguration>();
            var context = new Moq.Mock<IBrowsingContext>();
            _wikiGenerator = new WikipediaTextGenerator(config.Object, context.Object);
        }

        [TestCase("http://en.m.blah/test")]
        [TestCase("https://en.m.blah/test")]
        [TestCase("https://en.M.blah/test")]
        [TestCase("HTTPS://EN.M.BLAH/TEST")]
        public void wikipedia_text_gen_mobile_detected_correctly(string url)
        {
            Assert.That(_wikiGenerator.IsMobile(url), Is.True);
        }

        [Test]
        public void wikipedia_text_gen_mobile_title_retrieved_with_correct_selector()
        {
            var doc = new Moq.Mock<IDocument>();

            string TITLE = "mobile title";
            string url = "https://en.m.wikipedia.org/wiki/Nintendo";
            doc.Setup(s => s.Url).Returns(url);

            var mobileTitle = new Moq.Mock<IElement>();
            mobileTitle.Setup(s => s.TextContent).Returns(TITLE);
            doc.Setup(
                s => s.QuerySelector(
                    It.Is<string>(a => a.Equals(Constants.Wikipedia.ArticleTitle.TITLE_MOBILE))
                )
            ).Returns(mobileTitle.Object);

            doc.Setup(s => s.QuerySelectorAll(It.IsAny<string>())).Returns(default(IHtmlCollection<IElement>));

            var result = _wikiGenerator.ParseHtml(doc.Object);
            Assert.That(result.IsMobile, Is.True);
            Assert.That(result.Title, Is.EqualTo(TITLE));
        }

        [Test]
        public void wikipedia_text_gen_desktop_title_retrieved_with_correct_selector()
        {
            var doc = new Moq.Mock<IDocument>();
            
            string TITLE = "desktop title";
            string mobileUrl = "https://en.wikipedia.org/wiki/Nintendo";
            doc.Setup(s => s.Url).Returns(mobileUrl);

            var desktopTitle = new Moq.Mock<IElement>();
            desktopTitle.Setup(s => s.TextContent).Returns(TITLE);
            doc.Setup(
                s => s.QuerySelector(
                    It.Is<string>(a => a.Equals(Constants.Wikipedia.ArticleTitle.TITLE_DESKTOP))
                )
            ).Returns(desktopTitle.Object);

            var result = _wikiGenerator.ParseHtml(doc.Object);
            Assert.That(result.IsMobile, Is.False);
            Assert.That(result.Title, Is.EqualTo(TITLE));
        }
    }
}
