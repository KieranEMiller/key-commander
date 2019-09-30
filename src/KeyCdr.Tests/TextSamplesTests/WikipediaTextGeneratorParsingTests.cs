using KeyCdr.TextSamples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KeyCdr.Tests.TextSamplesTests
{
    [TestFixture]
    public class WikipediaTextGeneratorParsingTests
    {
        private string GetResourceTextByFileName(string fileName)
        {
            string prefix = "KeyCdr.Tests.TextSamplesTests.WikipediaParsingSamples.";
            string html = EmbeddedResources.LoadResourceAsString(prefix + fileName);
            return html;
        }

        [Test]
        public async Task wikipedia_load_sample_resource()
        {
            string html = GetResourceTextByFileName("wikipedia_sample_01_title_and_content_sections.html");

            var gen = new WikipediaTextGenerator();
            WikipediaTextResult result = await gen.GetWikipediaTextFromString(html);


            Console.WriteLine(result.Title);
            Console.WriteLine(result.Url);
            foreach (var section in result.TextSections)
                Console.WriteLine(section);
        }

        [Test]
        public async Task wikipedia_load_sample_resource2()
        {
            string resourceId = "KeyCdr.Tests.TextSamplesTests.WikipediaParsingSamples.wikipedia_sample_02_non_english_chars.html";
            string html = EmbeddedResources.LoadResourceAsString(resourceId);
            var gen = new WikipediaTextGenerator();
            WikipediaTextResult result = await gen.GetWikipediaTextFromString(html);
            Console.WriteLine(result.Title);
            Console.WriteLine(result.Url);
            foreach (var section in result.TextSections)
                Console.WriteLine(section);
        }
    }
}
