using KeyCdr.Data;
using KeyCdr.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeyCdr.UI.Web.Controllers
{
    public class SessionController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetNewSession(JWTToken token)
        {
            var user = new KCUser() { UserId = Guid.Parse(token.UserId) };
            var session = new KeyCdr.UserSession(user).StartNewSession();
            var newSession = new SessionModel() {
                SessionId = session.SessionId,
                UserId = session.UserId,
            };
            return Request.CreateResponse(HttpStatusCode.OK, newSession);
        }
    }
}