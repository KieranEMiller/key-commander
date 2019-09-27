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
            //split the string to eliminate groups of more than one spaces, there is probably
            //a more efficient way to do this; one option is regex but the current implementation
            //is easiest to understand in my opinion
            //TODO: improve efficiency
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string normalized = string.Join(" ", words);
            normalized = normalized.Replace("\t", string.Empty);
            normalized = normalized.Replace("\r\n", string.Empty);
            return normalized.Trim();
        }
    }
}
