using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class BaseTextSampleGenerator
    {
        public BaseTextSampleGenerator()
        {
            _rnd = new Random();
        }

        protected Random _rnd;
    }
}
