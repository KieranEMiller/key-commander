using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public interface ITextSampleGenerator
    {
        ITextSample GetWord();

        //TODO: probably need some way to handle restricted characters or sequences
        //string GetWord(IList<string> restrictions);

        ITextSample GetSentence();

        ITextSample GetParagraph();
    }
}
