﻿using KeyCdr.Analytics;
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
                details.AddRange(ProcessIncorrectChars(measurement));

                //the highlighted text for extra and short characters is similar; either
                //  1) short: length measured + number short or missing
                //  2) extra: length measured + number extra chars
                if(measurement.NumExtraChars > 0 || measurement.NumShortChars > 0)
                {
                    details.AddRange(ProcessExtraAndShortChars(measurement));
                }
            }

            return details;
        }

        public IList<HighlightDetail> ProcessIncorrectChars(AccuracyMeasurement measurement)
        {
            var details = new List<HighlightDetail>();
            foreach(var incorrectChar in measurement.IncorrectChars)
            {
                int indexStart = incorrectChar.Index + measurement.IndexInLargerText;
                details.Add(new HighlightDetail() {
                    HighlightType = HighlightType.IncorrectChar,
                    IndexStart = indexStart,
                    IndexEnd = indexStart + 1
                });
            }
            return details;
        }

        public IList<HighlightDetail> ProcessExtraAndShortChars(AccuracyMeasurement measurement)
        {
            var details = new List<HighlightDetail>();

            //are we working with extra or short chars
            bool isExtraChars = (measurement.NumExtraChars > 0);

            int delta = (isExtraChars) ? 
                measurement.NumExtraChars 
                : measurement.NumShortChars;

            int indexStart = measurement.LengthMeasured - delta;
            int indexEnd = indexStart + delta;

            details.Add(new HighlightDetail() {
                HighlightType = (isExtraChars) ? 
                    HighlightType.ExtraChars 
                    : HighlightType.ShortChars,
                IndexStart = indexStart,
                IndexEnd = indexEnd
            });

            return details;
        }
    }
}
