using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KeyCdr.UI.Web.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage History(JWTToken token)
        {
            var userMgr = new KeyCdr.History.UserSessionHistory();
            IList<Data.Session> history = userMgr.GetSessionsByUser(new Data.KCUser() { UserId = Guid.Parse(token.UserId) });
            return Request.CreateResponse(HttpStatusCode.OK, history);
        }
    }
}