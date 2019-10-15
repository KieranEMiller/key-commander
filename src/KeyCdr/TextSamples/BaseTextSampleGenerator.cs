using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class BaseTextSampleGenerator 
    {
        public const int NUM_SENTENCES_IN_A_PARAGRAPH = 4;

        public BaseTextSampleGenerator()
        {
            _rnd = new Random();
        }

        protected Random _rnd;

        public virtual TextSampleSourceType GetSourceType()
        {
            return TextSampleSourceType.Unknown;
        }

        protected string NormalizeText(string input)
        {
            //split the string to eliminate groups of more than one spaces, there is probably
            //a more efficient way to do this

            //method 1: split and join method
            //string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //string normalized = string.Join(" ", words);
            //normalized = normalized.Replace("\t", string.Empty);
            //normalized = normalized.Replace("\r\n", string.Empty);

            //method 2: regex method
            //remove non-ascii characters including control
            //TODO: improve how this works; with non english characters, they are 
            //removed but it leaves a weird trace, for ex: 
            //res (/riz/; Ancient Greek: , res [rs])
            input = Regex.Replace(input, @"[^\u0020-\u007E]", string.Empty);

            //remove duplicate spaces
            input = Regex.Replace(input, @"\s+", " ");

            return input.Trim();
        }

        protected string SplitAndGetARandomIndex(string text, char[] splitOn)
        {
            var items = text.Split(splitOn, StringSplitOptions.RemoveEmptyEntries);
            var randomIndex = GetRandomIndex(items);
            return items[randomIndex];
        }

        protected string GetWordFromTextBlock(string text)
        {
            string word = SplitAndGetARandomIndex(text, Constants.StringSplits.SEPARATOR_WORD);
            return word;
        }

        protected string GetSentenceFromTextBlock(string text)
        {
            string sentence = SplitAndGetARandomIndex(text, Constants.StringSplits.SEPARATOR_SENTENACE);
            return sentence;
        }

        public string GetParagraphFromTextBlock(string text)
        {
            return GetParagraphFromTextBlock(text, Constants.StringSplits.SEPARATOR_SENTENACE);
        }

        protected string GetParagraphFromTextBlock(string text, char[] splitOn)
        {
            var sentences = text.Split(splitOn, StringSplitOptions.RemoveEmptyEntries);

            int startIndex = 0;
            int endIndex = 0;
            if (sentences.Length <= NUM_SENTENCES_IN_A_PARAGRAPH)
            {
                endIndex = sentences.Length - 1;
            }
            else
            {
                int variability = sentences.Length - NUM_SENTENCES_IN_A_PARAGRAPH;
                startIndex = GetRandomIndex(variability);
                endIndex = startIndex + NUM_SENTENCES_IN_A_PARAGRAPH;
            }

            //TODO: when we rejoin the sentences we always use a period.  this is wrong
            //should preserve what the original text had there that was split on
            string DELIM = ". ";
            string paragraph = string.Join(". ", sentences
                .Skip(startIndex)
                .Take(endIndex - startIndex));

            //add a separator after the last sentence
            paragraph += DELIM;
            return paragraph.Trim();
        }

        protected int GetRandomIndex(string[] coll)
        {
            return GetRandomIndex(coll.Length);
        }

        protected int GetRandomIndex(IEnumerable<string> coll)
        {
            return GetRandomIndex(coll.Count());
        }

        public virtual int GetRandomIndex(int totalItems)
        {
            return _rnd.Next(0, totalItems);
        }
    }
}
