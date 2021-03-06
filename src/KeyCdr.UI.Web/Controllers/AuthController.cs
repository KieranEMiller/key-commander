﻿using System;
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
            if(dbUser == null || !userMgr.IsValidLogin(dbUser, user.password))
                return Request.CreateResponse(HttpStatusCode.Forbidden, result);

            result.JWTValue = new JWTManager().GenerateToken(dbUser.LoginName);
            result.IsValid = true;
            result.UserId = dbUser.UserId.ToString();

            userMgr.RecordLogin(dbUser, System.Web.HttpContext.Current);

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

        [HttpPost]
        public HttpResponseMessage IsUsernameInUse(WebUser user)
        {
            var userMgr = new Users.UserManager();
            var dbUser = userMgr.GetByLoginName(user.username);

            user.IsInUse = true;
            if (dbUser == null)
                user.IsInUse = false;

            return Request.CreateResponse(HttpStatusCode.BadRequest, user);
        }

        [HttpPost]
        public HttpResponseMessage Register(WebUser user)
        {
            var usermgr = new Users.UserManager();

            //todo: multiple db calls here, should really be one
            //create the user and save, then login which looks up the user again
            usermgr.CreateUser(user.username, user.password);
            return this.Login(user);
        }
    }
}