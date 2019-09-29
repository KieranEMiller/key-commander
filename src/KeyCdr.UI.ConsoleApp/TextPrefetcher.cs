using KeyCdr.TextSamples;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KeyCdr.UI.ConsoleApp
{
    public class TextPrefetcher
    {
        public int NUM_PREFETCHES_TO_KEEP_ON_HAND = 3;

        public TextPrefetcher()
        {
            _wikiTextGenerator = new WikipediaTextGenerator();
            _cachedData = new Queue<WikipediaTextResult>();

            Init();
            PrefetchMoreIfNeeded();
        }

        private WikipediaTextGenerator _wikiTextGenerator;
        private WikipediaTextResult _wikiText;

        private Queue<WikipediaTextResult> _cachedData;

        private void Init()
        {
            _wikiText = _wikiTextGenerator.GetWikipediaTextFromUrlSynchronously();
        }

        private async void PrefetchMoreIfNeeded()
        {
            for(int i=1; i <= NUM_PREFETCHES_TO_KEEP_ON_HAND - _cachedData.Count; i++)
            {
                var data = await _wikiTextGenerator.GetWikipediaTextFromUrl();
                
                _cachedData.Enqueue(data);
                if (_wikiText == null)
                    _wikiText = data;
            }
        }

        private string GetNextTextSection()
        {
            string textSection = string.Empty;
            int counter = 0;
            while(string.IsNullOrEmpty(textSection) && counter < 5)
            {
                if(_wikiText == null || _wikiText.TextSections.Count == 0)
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

        public string GetWord()
        {
            string text = GetNextTextSection();
            var simple = new SimpleTextGenerator(text);
            string word = simple.GetWord();
            return word;
        }
        
        public string GetSentence()
        {
            string text = GetNextTextSection();
            var simple = new SimpleTextGenerator(text);
            string sentence = simple.GetSentence();
            return sentence;
        }

        public string GetParagraph()
        {
            string text = GetNextTextSection();
            var simple = new SimpleTextGenerator(text);
            string paragraph = simple.GetParagraph();
            return paragraph;
        }
    }
}
