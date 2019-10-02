using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class WikipediaTextGenerator : BaseTextSampleGenerator, ITextSampleGenerator
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

        private WikipediaTextResult _wikiResult;

        public override TextSampleSourceType GetSourceType()
        {
            return TextSampleSourceType.Wikipedia;
        }

        public ITextSample TextToTextSample(string text, string sourceKey)
        {
            return new TextSample {
                Text = base.NormalizeText(text),

                //the article's url serves as the source key
                SourceKey = sourceKey,
                SourceType = GetSourceType()
            };
        }

        public ITextSample GetWord()
        {
            if (_wikiResult == null) _wikiResult = GetWikipediaTextFromUrlSynchronously();

            string text = _wikiResult.TextSections[base.GetRandomIndex(_wikiResult.TextSections)];
            string word = SplitAndGetARandomIndex(text, Constants.StringSplits.SEPARATOR_WORD);
            return TextToTextSample(text, _wikiResult.Url);
        }

        public ITextSample GetSentence()
        {
            if (_wikiResult == null) _wikiResult = GetWikipediaTextFromUrlSynchronously();

            string text = _wikiResult.TextSections[base.GetRandomIndex(_wikiResult.TextSections)];
            string sentence = SplitAndGetARandomIndex(text, Constants.StringSplits.SEPARATOR_SENTENACE);
            return TextToTextSample(text, _wikiResult.Url);
        }

        public ITextSample GetParagraph()
        {
            if (_wikiResult == null) _wikiResult = GetWikipediaTextFromUrlSynchronously();

            string text = _wikiResult.TextSections[base.GetRandomIndex(_wikiResult.TextSections)];
            text = base.GetParagraphFromTextBlock(text, Constants.StringSplits.SEPARATOR_SENTENACE);
            return TextToTextSample(text, _wikiResult.Url);
        }

        public WikipediaTextResult GetWikipediaTextFromUrlSynchronously()
        {
            Task<WikipediaTextResult> task = Task.Run<WikipediaTextResult>(async () => await GetWikipediaTextFromUrl());
            return task.Result;
        }

        public async Task<WikipediaTextResult> GetWikipediaTextFromString(string html)
        {
            var document = await _angleSharpContext.OpenAsync(req => req.Content(html));
            WikipediaTextResult result = ParseHtml(document);
            return result;
        }

        public async Task<WikipediaTextResult> GetWikipediaTextFromUrl()
        {
            return await GetWikipediaTextFromUrl(URL_RANDOM_PAGE);
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
                result.AddText(base.NormalizeText(section));

            return result;
        }

        public string ExtractTitle(AngleSharp.Dom.IDocument doc, bool isMobile)
        {
            AngleSharp.Dom.IElement cell;
            cell = isMobile ? doc.QuerySelector(Constants.Wikipedia.ArticleTitle.TITLE_MOBILE)
                        : doc.QuerySelector(Constants.Wikipedia.ArticleTitle.TITLE_DESKTOP);
            return cell != null ? cell.TextContent : string.Empty;
        }

        public IEnumerable<string> ExtractTextSections(AngleSharp.Dom.IDocument doc, bool isMobile)
        {
            var cells = doc.QuerySelectorAll(Constants.Wikipedia.Paragraphs.PARAGRAPHS);
            IEnumerable<string> textSections = Enumerable.Empty<string>();
            if(cells != null && cells.Length > 0)
                textSections = cells.Select(m => m.TextContent);
            return textSections;
        }

        public bool IsMobile(string url)
        {
            return Regex.IsMatch(url, Constants.Wikipedia.REGEX_MOBILE_URL, RegexOptions.IgnoreCase);
        }
    }
}
