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
        [HttpGet]
        public HttpResponseMessage GetNewSequence()
        {
            WikipediaTextGenerator gen = new WikipediaTextGenerator();
            var sample = gen.GetParagraph();

            var result = new NewSequenceModel() {
                Text = sample.GetText(),
                SourceKey = sample.GetSourceKey(),
                SourceType = sample.GetSourceType().ToString()
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}