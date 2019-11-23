using KeyCdr.Analytics;
using KeyCdr.Data;
using KeyCdr.Highlighting;
using KeyCdr.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeyCdr.UI.Web.Controllers
{
    public class ErrorAnalysisController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage RunForSequence(SequenceModel sequence)
        {
            if (string.IsNullOrWhiteSpace(sequence.TextEntered))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Empty);

            KeySequence keySeq = new History.UserSessionHistory().GetHistoryDetailsByKeySequence(sequence.SequenceId);
            Accuracy accuracy = new Accuracy(new AnalyticData() {
                TextShown = keySeq.TextShown,
                TextEntered = keySeq.TextEntered
            });
            accuracy.Compute();

            IList<HighlightDetail> details = new HighlightDeterminator().Compute(accuracy);
            HighlightedTextBuilder highlightBuilder = new HighlightedTextBuilder();
            Queue<HighlightedText> highlightedSections = highlightBuilder.Compute(keySeq.TextShown, details);

            return Request.CreateResponse(HttpStatusCode.OK, highlightedSections.ToList());
        }
    }
}