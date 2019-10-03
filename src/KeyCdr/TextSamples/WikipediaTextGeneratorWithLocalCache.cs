using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class WikipediaTextGeneratorWithLocalCache : WikipediaTextGenerator, ITextSampleGenerator
    {
        public int NUM_PREFETCHES_TO_KEEP_ON_HAND = 3;

        public WikipediaTextGeneratorWithLocalCache()
            : base()
        {
            Init();
        }

        public WikipediaTextGeneratorWithLocalCache(IConfiguration config, IBrowsingContext context)
            : base(config, context)
        {
            Init();
        }

        private WikipediaTextResult _wikiText;
        private Queue<WikipediaTextResult> _cachedData;

        public async void Init()
        {
            _wikiText = await base.GetWikipediaTextFromUrl();
            _cachedData = new Queue<WikipediaTextResult>();

            PrefetchMoreIfNeeded();
        }

        public async void PrefetchMoreIfNeeded()
        {
            for (int i = 1; i <= NUM_PREFETCHES_TO_KEEP_ON_HAND - _cachedData.Count; i++)
            {
                var data = await base.GetWikipediaTextFromUrl();

                _cachedData.Enqueue(data);
                if (_wikiText == null)
                    _wikiText = data;
            }
        }

        public string GetNextTextSection()
        {
            string textSection = string.Empty;
            int counter = 0;
            while (string.IsNullOrEmpty(textSection) && counter < 5)
            {
                if (_wikiText == null || _wikiText.TextSections.Count == 0)
                {
                    _wikiText = _cachedData.Dequeue();
                }
                else
                {
                    textSection = _wikiText.TextSections[0];
                    _wikiText.TextSections.RemoveAt(0);
                    break;
                }

                counter++;
            }

            PrefetchMoreIfNeeded();

            return textSection;
        }

        public override ITextSample GetWord()
        {
            string text = GetNextTextSection();
            var simple = new SimpleTextGenerator(text);
            return simple.GetWord();
        }

        public override ITextSample GetSentence()
        {
            string text = GetNextTextSection();
            var simple = new SimpleTextGenerator(text);
            return simple.GetSentence();
        }

        public override ITextSample GetParagraph()
        {
            string text = GetNextTextSection();
            var simple = new SimpleTextGenerator(text);
            return simple.GetParagraph();
        }
    }
}
