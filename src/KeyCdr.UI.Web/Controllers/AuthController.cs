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
            JWTToken result = new JWTToken() {
                UserName = user.username,
                IsValid = false
            };

            var userMgr = new Users.UserManager();
            var dbUser = userMgr.GetByLoginName(user.username);
            if (dbUser == null)
                return Request.CreateResponse(HttpStatusCode.Forbidden, result);

            if(user.password.Equals("asdfasdf") == false)
                return Request.CreateResponse(HttpStatusCode.Forbidden, result);

            result.JWTValue = new JWTManager().GenerateToken(user.username);
            result.IsValid = true;
            result.UserId = dbUser.UserId.ToString();

            userMgr.RecordLogin(dbUser);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Validate(JWTToken token)
        {
            JWTToken result = new JWTToken() { IsValid = false };

            KeyCdr.Users.UserManager userMgr = new Users.UserManager();
            var dbUser = userMgr.GetByLoginName(token.UserName);
            if (dbUser == null)
                return Request.CreateResponse(HttpStatusCode.Forbidden, result);

            string tokenUsername = new JWTManager().ValidateToken(token.JWTValue);
            result.UserName = tokenUsername;
            if (token.UserName.Equals(tokenUsername)) {
                result.IsValid = true;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
    }
}