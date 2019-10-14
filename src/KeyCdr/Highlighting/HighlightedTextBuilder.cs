using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Highlighting
{
    public class HighlightedTextBuilder
    {
        public Queue<HighlightedText> Compute(string origText, IList<HighlightDetail> details)
        {
            Queue<HighlightedText> textSections = new Queue<HighlightedText>();

            int lastIndex = 0;
            foreach (HighlightDetail highlight in details.OrderBy(d => d.IndexStart))
            {
                string firstTextSection = origText.Substring(lastIndex, highlight.IndexStart);
                if(!string.IsNullOrWhiteSpace(firstTextSection)) {
                    HighlightedText nextUnformattedBlock = new HighlightedText() {
                        Text = firstTextSection,
                        HighlightType = HighlightType.Normal
                    };
                    textSections.Enqueue(nextUnformattedBlock);
                }

                HighlightedText formattedBlock = new HighlightedText() {
                    Text = origText.Substring(highlight.IndexStart, highlight.IndexEnd - highlight.IndexStart),
                    HighlightType = highlight.HighlightType
                };
                textSections.Enqueue(formattedBlock);

                lastIndex += highlight.IndexEnd - highlight.IndexStart;

                if(!string.IsNullOrWhiteSpace(firstTextSection))
                    lastIndex++;
            }

            HighlightedText lastUnformattedBlock = new HighlightedText() {
                Text=origText.Substring(lastIndex, origText.Length - lastIndex),
                HighlightType = HighlightType.Normal
            };
            textSections.Enqueue(lastUnformattedBlock);

            return textSections;
        }
    }
}
