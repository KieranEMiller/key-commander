using KeyCdr.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.KeySequences
{
    public interface IMeasuredKeySequence
    {
        void Start(string sequence);

        IList<IAnalytic> Peek(string enteredText);
        IList<IAnalytic> Peek(string enteredText, TimeSpan elapsed);

        IList<IAnalytic> Stop(string enteredText);
        IList<IAnalytic> Stop(string enteredText, TimeSpan elapsed);
    }
}
