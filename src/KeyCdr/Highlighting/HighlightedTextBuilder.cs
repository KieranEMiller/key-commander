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
                //if the lastIndex is equal to the index start then we don't need to add an 
                //unformatted block because the highlight starts right away
                if(lastIndex != highlight.IndexStart)
                {
                    HighlightedText nextUnformattedBlock = new HighlightedText() {
                        Text = origText.Substring(lastIndex, highlight.IndexStart),
                        HighlightType = HighlightType.Normal
                    };
                    textSections.Enqueue(nextUnformattedBlock);
                }

                HighlightedText formattedBlock = new HighlightedText() {
                    Text = origText.Substring(highlight.IndexStart, highlight.IndexEnd - highlight.IndexStart),
                    HighlightType = highlight.HighlightType
                };
                textSections.Enqueue(formattedBlock);

                //lastIndex += highlight.IndexEnd;
                lastIndex = highlight.IndexEnd;
            }

            //if the last index is greater or equal to the original texts length
            //then the last formatted block went to the end of the string 
            //or over in the case of extra chars
            if(lastIndex < origText.Length)
            {
                HighlightedText lastUnformattedBlock = new HighlightedText() {
                    Text=origText.Substring(lastIndex, origText.Length - lastIndex),
                    HighlightType = HighlightType.Normal
                };
                textSections.Enqueue(lastUnformattedBlock);
            }

            return textSections;
        }
    }
}
