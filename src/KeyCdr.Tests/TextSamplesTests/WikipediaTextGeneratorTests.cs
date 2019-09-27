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
            var textSections = await gen.GetIt();
            string qwer = "qwer";
        }
    }
}
