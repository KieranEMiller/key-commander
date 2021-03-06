﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KeyCdr.Data;

namespace KeyCdr.Users
{
    public class UserManager: BaseDBAccess
    {
        public UserManager()
        { }

        public KCUser GetByLoginName(string loginName)
        {
            KCUser user = _db.KCUser
                .Where(u => u.LoginName.Equals(loginName))
                .FirstOrDefault();

            return user;
        }

        public bool IsValidLogin(string loginName, string password)
        {
            var user = this.GetByLoginName(loginName);
            return IsValidLogin(user, password);
        }

        public bool IsValidLogin(KCUser user, string password)
        {
            if (user == null) return false;

            //if the password salt or hash is null then let the user login 
            //regardless of password
            //this is probably a huge security hole...
            if (user.PasswordSalt == null || user.PasswordHash == null)
                return true;

            UserAuthenticationSecurity sec = new UserAuthenticationSecurity();
            bool isValidLogin = sec.IsValid(new UserAuthenticationDigest() {
                Hash = Convert.FromBase64String(user.PasswordHash),
                Salt = Convert.FromBase64String(user.PasswordSalt)
            }, password);

            return isValidLogin;
        }

        public KCUserLogin RecordLogin(KCUser user, HttpContext context)
        {
            KCUserLogin newLogin = new KCUserLogin();
            newLogin.KCUserLoginId = Guid.NewGuid();
            newLogin.UserId = user.UserId;
            newLogin.Created = DateTime.Now;
            newLogin.IpAddress = GetIpAddress(context);
            newLogin.UserAgent = GetUserAgent(context);

            _db.KCUserLogin.Add(newLogin);
            _db.SaveChanges();

            return newLogin;
        }

        public KCUser CreateUser(string username, string password)
        {
            KCUser user = new KCUser();
            user.UserId = Guid.NewGuid();
            user.Name = username;
            user.LoginName = username;
            user.Created = DateTime.Now;

            if(string.IsNullOrEmpty(password)) {
                user.PasswordHash = null;
                user.PasswordSalt = null;
            }
            else {
                var digest = new Users.UserAuthenticationSecurity().Hash(password);
                user.PasswordHash = digest.HashBase64;
                user.PasswordSalt = digest.SaltBase64;
            }

            _db.KCUser.Add(user);
            _db.SaveChanges();

            return user;
        }

        public KCUser CreateGuest()
        {
            string guestname = GetNewGuestName();
            return CreateUser(guestname, null);
        }

        public string GetNewGuestName()
        {
            return string.Format("guest_{0}", DateTime.UtcNow.Ticks);
        }

        public string GetIpAddress(HttpContext context)
        {
            string ip = context.Request.ServerVariables["X_FORWARDED_FOR"];

            if (string.IsNullOrWhiteSpace(ip))
                ip = context.Request.UserHostAddress;

            return ip;
        }

        public string GetUserAgent(HttpContext context)
        {
            if (context.Request.UserAgent.Length > 500)
                return context.Request.UserAgent.Substring(0, 500);

            return context.Request.UserAgent;
        }
    }
}
