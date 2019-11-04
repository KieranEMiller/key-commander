using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeyCdr.UI.Web.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(WebUser user)
        {
            JWTResult result = new JWTResult() {
                username = user.username,
                isvalid = false
            };

            var dbUser = new Users.UserManager().GetByLoginName(user.username);
            if (dbUser == null)
                return Request.CreateResponse(HttpStatusCode.Forbidden, result);

            if(user.password.Equals("asdfasdf") == false)
                return Request.CreateResponse(HttpStatusCode.Forbidden, result);

            result.jwt = new JWTManager().GenerateToken(user.username);
            result.isvalid = true;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Validate(JWTResult token)
        {
            JWTResult result = new JWTResult() { isvalid = false };

            KeyCdr.Users.UserManager userMgr = new Users.UserManager();
            var dbUser = userMgr.GetByLoginName(token.username);
            if (dbUser == null)
                return Request.CreateResponse(HttpStatusCode.Forbidden, result);

            string tokenUsername = new JWTManager().ValidateToken(token.jwt);
            result.username = tokenUsername;
            if (token.username.Equals(tokenUsername)) {
                result.isvalid = true;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
    }
}