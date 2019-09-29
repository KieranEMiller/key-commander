using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.TextSamples
{
    public class WikipediaTextResult
    {
        private const int MIN_TEXT_SECTION_CHAR_LENGTH = 30;

        public WikipediaTextResult()
        {
            this.TextSections = new List<string>();
        }

        public string Url { get; set; }
        public string Title { get; set; }
        public bool IsMobile { get; set; }

        public List<string> TextSections { get; set; }

        public void AddText(string text)
        {
            //clean up, normalize text
            text = text.Trim();

            if (!string.IsNullOrWhiteSpace(text)
                && text.Length > MIN_TEXT_SECTION_CHAR_LENGTH)
            {
                this.TextSections.Add(text);
            }
        }

        public bool HasSufficientDataForUse()
        {
            return !string.IsNullOrEmpty(this.Title)
                && !string.IsNullOrEmpty(this.Url)
                && this.TextSections.Count > 0;
        }
    }
}
