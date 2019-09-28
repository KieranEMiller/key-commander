using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class WikipediaTextGenerator : SimpleTextGenerator, ITextSampleGenerator
    {
        public const string URL_RANDOM_PAGE = "https://en.wikipedia.org/wiki/Special:Random";

        public WikipediaTextGenerator()
        {
            _angleSharpConfig = Configuration.Default.WithDefaultLoader();
            _angleSharpContext = BrowsingContext.New(_angleSharpConfig);
        }

        public WikipediaTextGenerator(IConfiguration config, IBrowsingContext context)
        {
            _angleSharpConfig = config;
            _angleSharpContext = context;
        }

        private IConfiguration _angleSharpConfig;
        private IBrowsingContext _angleSharpContext;

        public async Task<WikipediaTextResult> GetWikipediaTextFromString(string html)
        {
            var document = await _angleSharpContext.OpenAsync(req => req.Content(html));
            WikipediaTextResult result = ParseHtml(document);
            return result;
        }

        public async Task<WikipediaTextResult> GetWikipediaTextFromUrl(string url)
        {
            AngleSharp.Dom.IDocument document = await _angleSharpContext.OpenAsync(url);
            WikipediaTextResult result = ParseHtml(document);
            return result;
        }

        public WikipediaTextResult ParseHtml(AngleSharp.Dom.IDocument document)
        {
            bool isMobile = IsMobile(document.Url);
            var result = new WikipediaTextResult
            {
                Url = document.Url,
                IsMobile = isMobile,
                Title = ExtractTitle(document, isMobile)
            };

            IEnumerable<string> textSections = ExtractTextSections(document, isMobile);
            foreach (var section in textSections)
                result.AddText(section);

            return result;
        }

        public string ExtractTitle(AngleSharp.Dom.IDocument doc, bool isMobile)
        {
            AngleSharp.Dom.IElement cell;
            cell = isMobile ? doc.QuerySelector(Constants.Wikipedia.ArticleTitle.TITLE_MOBILE)
                        : doc.QuerySelector(Constants.Wikipedia.ArticleTitle.TITLE_DESKTOP);
            return cell.TextContent;
        }

        public IEnumerable<string> ExtractTextSections(AngleSharp.Dom.IDocument doc, bool isMobile)
        {
            var cells = doc.QuerySelectorAll(Constants.Wikipedia.Paragraphs.PARAGRAPHS);
            var text = cells.Select(m => m.TextContent);
            return text;
        }

        public bool IsMobile(string url)
        {
            return Regex.IsMatch(url, Constants.Wikipedia.REGEX_MOBILE_URL, RegexOptions.IgnoreCase);
        }
    }
}
