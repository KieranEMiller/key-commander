using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class WikipediaTextGenerator : SimpleTextGenerator, ITextSampleGenerator
    {
        public const string URL = "https://en.wikipedia.org/wiki/Special:Random";

        public WikipediaTextGenerator()
        {
            
        }

        public async Task<IEnumerable<string>> GetIt()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(URL);
            var cellSelector = "div#mw-content-text p";
            var cells = document.QuerySelectorAll(cellSelector);
            
            var titles = cells.Select(m => m.TextContent);
            return titles;
        }
    }
}
