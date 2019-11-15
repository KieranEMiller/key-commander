using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public interface IAnalyticRecordable
    {
        void Record(KeyCdr.Data.KeyCdrDataEntities db, KeyCdr.Data.KeySequenceAnalysis seqAnalysis);
    }
}
