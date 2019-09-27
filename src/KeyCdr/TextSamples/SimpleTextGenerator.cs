using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class SimpleTextGenerator : BaseTextSampleGenerator, ITextSampleGenerator
    {
        public const int NUM_SENTENCES_IN_A_PARAGRAPH = 3;
        
        public SimpleTextGenerator()
        { }

        public SimpleTextGenerator(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        protected string SplitAndGetARandomIndex(char[] splitOn)
        {
            var items = this.Text.Split(splitOn, StringSplitOptions.RemoveEmptyEntries);
            var randomIndex = _rnd.Next(0, items.Length - 1);
            return items[randomIndex];
        }

        public string GetWord()
        {
            string word = SplitAndGetARandomIndex(Constants.StringSplits.SEPARATOR_WORD);
            return base.NormalizeText(word);
        }

        public string GetSentence()
        {
            string sentence = SplitAndGetARandomIndex(Constants.StringSplits.SEPARATOR_SENTENACE);
            return base.NormalizeText(sentence);
        }

        public string GetParagraph()
        {
            var sentences = this.Text.Split(Constants.StringSplits.SEPARATOR_SENTENACE, StringSplitOptions.RemoveEmptyEntries);

            int startIndex = 0;
            int endIndex = 0;
            if(sentences.Length <= NUM_SENTENCES_IN_A_PARAGRAPH)
            {
                endIndex = sentences.Length - 1;
            }
            else
            {
                int variability = sentences.Length - NUM_SENTENCES_IN_A_PARAGRAPH;
                startIndex = _rnd.Next(0, variability);
                endIndex = startIndex + NUM_SENTENCES_IN_A_PARAGRAPH; 
            }

            //TODO: when we rejoin the sentences we always use a period.  this is wrong
            //should preserve what the original text had there that was split on
            string DELIM = ". ";
            string paragraph = string.Join(". ", sentences
                .Skip(startIndex)
                .Take(endIndex - startIndex));

            //add a separator after the last sentence
            paragraph += DELIM;
                
            return NormalizeText(paragraph);
        }
    }
}
