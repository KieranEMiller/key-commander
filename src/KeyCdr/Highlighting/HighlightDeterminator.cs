using KeyCdr.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Highlighting
{
    public class HighlightDeterminator
    {
        public HighlightDeterminator()
        { }

        public IList<HighlightDetail> Compute(Accuracy accuracy)
        {
            var details = new List<HighlightDetail>();

            if (accuracy == null || accuracy.Measurements == null)
                return details;

            foreach(var measurement in accuracy.Measurements)
            {
                foreach(var incorrectChar in measurement.IncorrectChars)
                {
                    int indexStart = incorrectChar.Index + measurement.IndexInLargerText;
                    details.Add(new HighlightDetail() {
                        HighlightType = HighlightType.IncorrectChar,
                        IndexStart = indexStart,
                        IndexEnd = indexStart + 1
                    });
                }

                if(measurement.NumExtraChars > 0 || measurement.NumShortChars > 0)
                {
                    //the highlighted text for extra and short characters is similar; either
                    //  1) short: length measured + number short or missing
                    //  2) extra: length measured + number extra chars
                    int indexStart = measurement.LengthMeasured - 1;
                    int indexEnd = indexStart + ((measurement.NumExtraChars > 0) ? 
                        measurement.NumExtraChars 
                        : measurement.NumShortChars);

                    details.Add(new HighlightDetail() {
                        HighlightType = HighlightType.ExtraChars,
                        IndexStart = indexStart,
                        IndexEnd = indexEnd
                    });
                }
            }

            return details;
        }

    }
}
