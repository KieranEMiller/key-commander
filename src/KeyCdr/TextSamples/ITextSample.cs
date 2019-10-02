using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public interface ITextSample
    {
        string GetText();
        string GetSourceKey();
        TextSampleSourceType GetSourceType();
    }
}
