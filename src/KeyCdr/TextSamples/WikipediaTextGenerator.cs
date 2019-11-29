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

        public virtual ITextSample GetWord()
        {
            ITextSample result = ExtractTextWithFunction(
                base.SplitAndGetARandomIndex, 
                Constants.StringSplits.SEPARATOR_WORD
            );

            return result ?? base.GetWordFallback();
        }

        public virtual ITextSample GetSentence()
        {
            ITextSample result = ExtractTextWithFunction(
                base.SplitAndGetARandomIndex, 
                Constants.StringSplits.SEPARATOR_SENTENACE
            );

            return result ?? base.GetSentenceFallback();
        }

        public virtual ITextSample GetParagraph()
        {
            ITextSample result = ExtractTextWithFunction(
                base.GetParagraphFromTextBlock, 
                Constants.StringSplits.SEPARATOR_SENTENACE
            );

            return result ?? base.GetParagraphFallback();
        }

        public ITextSample ExtractTextWithFunction(Func<string, char[], string> ParsingFunction, char[] separator)
        {
            if (_wikiResult == null) _wikiResult = GetWikipediaTextFromUrlSynchronously();

            if (_wikiResult == null || _wikiResult.TextSections.Count == 0)
                return null;

            string text = _wikiResult.TextSections[base.GetRandomIndex(_wikiResult.TextSections)];
            text = ParsingFunction(text, separator);
            return TextToTextSample(text, _wikiResult.Url);
        }

        public virtual WikipediaTextResult GetWikipediaTextFromUrlSynchronously()
        {
            Task<WikipediaTextResult> task = Task.Run<WikipediaTextResult>(async () => await GetWikipediaTextFromUrl());
            _wikiResult = task.Result;
            return task.Result;
        }

        public WikipediaTextResult GetWikipediaTextFromUrlSynchronously(string url)
        {
            Task<WikipediaTextResult> task = Task.Run<WikipediaTextResult>(async () => await GetWikipediaTextFromUrl(url));
            _wikiResult = task.Result;
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
