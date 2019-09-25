using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class HardCodedTextGenerator : BaseTextSampleGenerator, ITextSampleGenerator
    {
        //from the first paragraph of moby dick to use as a sample
        private string _sampleParagraph = @"
            Call me Ishmael. Some years ago—never mind how long precisely—having
            little or no money in my purse, and nothing particular to interest me
            on shore, I thought I would sail about a little and see the watery part
            of the world. It is a way I have of driving off the spleen and
            regulating the circulation.Whenever I find myself growing grim about
            the mouth; whenever it is a damp, drizzly November in my soul; whenever
            I find myself involuntarily pausing before coffin warehouses, and
            bringing up the rear of every funeral I meet; and especially whenever
            my hypos get such an upper hand of me, that it requires a strong moral
            principle to prevent me from deliberately stepping into the street, and
            methodically knocking people’s hats off—then, I account it high time to
            get to sea as soon as I can.";

        private string SplitAndGetARandomIndex(char[] splitOn)
        {
            var items = _sampleParagraph.Split(splitOn, StringSplitOptions.RemoveEmptyEntries);
            var randomIndex = _rnd.Next(0, items.Length - 1);
            return items[randomIndex];
        }

        public string GetWord()
        {
            string word = SplitAndGetARandomIndex(new char[] { ' ' });
            return base.NormalizeText(word);
        }

        public string GetSentence()
        {
            string sentence = SplitAndGetARandomIndex(new char[] { '.', ';' });
            return base.NormalizeText(sentence);
        }

        public string GetParagraph()
        {
            return NormalizeText(_sampleParagraph);
        }

    }
}
