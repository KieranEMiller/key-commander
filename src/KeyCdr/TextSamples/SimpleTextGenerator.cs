using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class SimpleTextGenerator : BaseTextSampleGenerator, ITextSampleGenerator
    {
        
        public SimpleTextGenerator()
        { }

        public SimpleTextGenerator(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public string GetWord()
        {
            string word = base.SplitAndGetARandomIndex(this.Text, Constants.StringSplits.SEPARATOR_WORD);
            return base.NormalizeText(word);
        }

        public string GetSentence()
        {
            string sentence = base.SplitAndGetARandomIndex(this.Text, Constants.StringSplits.SEPARATOR_SENTENACE);
            return base.NormalizeText(sentence);
        }

        public string GetParagraph()
        {
            string paragraph = base.GetParagraphFromTextBlock(this.Text, Constants.StringSplits.SEPARATOR_SENTENACE);
            return base.NormalizeText(paragraph);
        }
    }
}
