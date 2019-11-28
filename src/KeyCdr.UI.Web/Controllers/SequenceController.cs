using KeyCdr.Analytics;
using KeyCdr.Data;
using KeyCdr.KeySequences;
using KeyCdr.TextSamples;
using KeyCdr.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeyCdr.UI.Web.Controllers
{
    public class SequenceController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetNewSequence(SessionModel session)
        {
            WikipediaTextGenerator gen = new WikipediaTextGenerator();
            var sample = gen.GetParagraph();

            KCUser user = new KCUser() { UserId = session.UserId };
            UserSession sessionMgr = new UserSession(user);
            KeySequence seq = sessionMgr.StartNewSequence(sample);

            var result = new SequenceModel() {
                SessionId = seq.SessionId,
                SequenceId = seq.KeySequenceId,

                Text = sample.GetText(),
                SourceKey = sample.GetSourceKey(),
                SourceType = sample.GetSourceType().ToString()
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage EndSequence(SequenceModel seqFromClient)
        {
            //ideally here we look up the elapsed time based on whats in the db 
            //and not rely on them to report the time used but...
            //after all: manipulating this only hurts the user if they want to cheat
            var timeSpan = TimeSpan.FromMilliseconds(seqFromClient.ElapsedInMilliseconds);

            //ignore the results from the call to stop as for now the user is redirected
            //to the details page
            UserSessionStateless sessionMgr = new UserSessionStateless();
            sessionMgr.StopSequence(seqFromClient.SequenceId, seqFromClient.TextEntered, timeSpan);

            return Request.CreateResponse(HttpStatusCode.OK, string.Empty);
        }
    }
}