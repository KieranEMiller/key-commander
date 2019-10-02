using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class TextSample : ITextSample
    {
        public string Text { get; set; }
        public TextSampleSourceType SourceType { get; set; }
        public string SourceKey { get; set; }

        public string GetSourceKey()
        { return this.SourceKey; }

        public TextSampleSourceType GetSourceType()
        { return this.SourceType; }

        public string GetText()
        { return this.Text; }
    }
}
