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

        protected string NormalizeText(string input)
        {
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string normalized = string.Join(" ", words);
            normalized = normalized.Replace("\t", string.Empty);
            normalized = normalized.Replace("\r\n", string.Empty);
            return normalized.Trim();
        }
    }
}
