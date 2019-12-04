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
        public HttpResponseMessage RunForSequence(SequenceModel sequenceFromClient)
        {
            KeySequence keySeq = new History.UserSessionHistory().GetHistoryDetailsByKeySequence(sequenceFromClient.SequenceId);

            //if the text entered from the db and the text from the client is 
            //empty or null then return analysis unavailable
            if (string.IsNullOrWhiteSpace(keySeq.TextEntered)
                && string.IsNullOrWhiteSpace(sequenceFromClient.TextEntered))
            {
                var invalid =  new List<HighlightedText>() { new HighlightedText() {
                    HighlightType = HighlightType.Unevaluated,
                    Text = "No analysis available for this sequence.  No user provided text detected."
                }};
                return Request.CreateResponse(HttpStatusCode.OK, invalid);
            }

            Accuracy accuracy = new Accuracy(new AnalyticData() {
                TextShown = keySeq.TextShown,
                TextEntered = 
                    (string.IsNullOrWhiteSpace(keySeq.TextEntered))
                    ? sequenceFromClient.TextEntered : keySeq.TextEntered
            });
            accuracy.Compute();

            IList<HighlightDetail> details = new HighlightDeterminator().Compute(accuracy);
            HighlightedTextBuilder highlightBuilder = new HighlightedTextBuilder();
            Queue<HighlightedText> highlightedSections = highlightBuilder.Compute(keySeq.TextShown, details);

            return Request.CreateResponse(HttpStatusCode.OK, highlightedSections.ToList());
        }
    }
}