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
        public async Task sample_resource_basic_parsing_and_markup_removal()
        {
            string html = GetResourceTextByFileName("wikipedia_sample_01_title_and_content_sections.html");

            var gen = new WikipediaTextGenerator();
            WikipediaTextResult result = await gen.GetWikipediaTextFromString(html);

            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.TextSections.Count, Is.EqualTo(3));
            Assert.That(result.TextSections, Is.EquivalentTo(new List<string>(){
                "Brian Booth State Park is a coastal recreational area located near Seal Rock, Oregon. It consists of two major portions: Ona Beach State Park and Beaver Creek State Natural Area. The park has beach access, kayaking, and hiking trails. The park is 886.32 acres and has an annual attendance of 247,772 people. Ona is known as a Chinook Jargon word for razor clam.[2]",
                "Ona Beach State Park is a state park in Lincoln County, Oregon United States, administered by the Oregon Parks and Recreation Department. It is located straddling Beaver Creek, which is approximately halfway between Newport and Waldport, Oregon, which is 9 miles (14km) south of the former.",
                "The land was purchased between 1938 and 1968 from private owners, and includes one gift of 10 acres from Lincoln County, Oregon made in 1963. In the days before the completion of the Coast Highway, the beach between Newport and Seal Rock was used as an access road. Motorists would travel at low tide, following the mail carrier who knew the best way to cross Beaver Creek. The park was first known as Ona Beach State Park, but was renamed in 2013 to honor the first Oregon State Parks and Recreation Commission chairperson Brian Booth (c. 1937March 2012)[3][4].[5]"
            }));
        }

        [Test]
        public async Task sample_resource_non_english_characters_stripped()
        {
            string html = GetResourceTextByFileName("wikipedia_sample_02_non_english_chars.html");

            var gen = new WikipediaTextGenerator();
            WikipediaTextResult result = await gen.GetWikipediaTextFromString(html);

            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.TextSections.Count, Is.EqualTo(2));
            Assert.That(result.TextSections, Is.EquivalentTo(new List<string>(){
                "Ares (/riz/; Ancient Greek: , res [rs]) is the Greek god of war. He is one of the Twelve Olympians, the son of Zeus and Hera.[1] In Greek literature, he often represents the physical or violent and untamed aspect of war, in contrast to his sister, the armored Athena, whose functions as a goddess of intelligence include military strategy and generalship.[2]",
                "Ares (/riz/; Ancient Greek: , res [rs]) is the Greek god of war. He is one of the Twelve Olympians, the son of Zeus and Hera.[1] In Greek literature, he often represents the physical or violent and untamed aspect of war, in contrast to his sister, the armored Athena, whose functions as a goddess of intelligence include military strategy and generalship.[2][4].[5]",
            }));
        }
    }
}
