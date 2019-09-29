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
    public class WikipediaTextGeneratorTests
    {
        //[Test]
        public async Task test()
        {
            var gen = new WikipediaTextGenerator();

            WikipediaTextResult result = null;
            for (int i = 0; i < 10; i++)
            {
                result = await gen.GetWikipediaTextFromUrl();
                Console.WriteLine(result.Title);
                Console.WriteLine(result.Url);
                foreach (var section in result.TextSections)
                   Console.WriteLine(string.Format("**** {0} ******", section));
            }
        }

        //[Test]
        public async Task wikipedia_load_sample_resource()
        {
            string resourceId = "KeyCdr.Tests.TextSamplesTests.WikipediaParsingSamples.wikipedia_sample_01_title_and_content_sections.html";
            string html = EmbeddedResources.LoadResourceAsString(resourceId);
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
