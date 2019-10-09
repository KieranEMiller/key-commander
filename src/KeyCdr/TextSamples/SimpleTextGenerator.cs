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

        public ITextSample TextToTextSample(string text)
        {
            return new TextSample {
                Text = base.NormalizeText(text),
                SourceKey = "SimpleText",
                SourceType = base.GetSourceType()
            };
        }
        
        public ITextSample GetWord()
        {
            string word = base.GetWordFromTextBlock(this.Text);
            return TextToTextSample(word);
        }

        public ITextSample GetSentence()
        {
            string sentence = base.GetSentenceFromTextBlock(this.Text);
            return TextToTextSample(sentence);
        }

        public ITextSample GetParagraph()
        {
            string paragraph = base.GetParagraphFromTextBlock(this.Text);
            return TextToTextSample(paragraph);
        }
    }
}
