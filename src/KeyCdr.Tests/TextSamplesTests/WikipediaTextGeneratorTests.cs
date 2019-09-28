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
        [Test]
        public async Task test()
        {
            var gen = new WikipediaTextGenerator();

            WikipediaTextResult result = null;
            for (int i = 0; i < 10; i++)
            {
                result = await gen.GetWikipediaText();
                Console.WriteLine(result.Title);
                Console.WriteLine(result.Url);
                foreach (var section in result.TextSections)
                    Console.WriteLine(section);
            }
        }
    }
}
