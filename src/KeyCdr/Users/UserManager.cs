using System;
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
        {
        }

        public KCUser GetByLoginName(string loginName)
        {
            KCUser user = _db.KCUser
                .Where(u => u.LoginName.Equals(loginName))
                .FirstOrDefault();

            return user;
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

        public KCUser CreateGuest()
        {
            KCUser user = new KCUser();
            user.UserId = Guid.NewGuid();

            string guestname = GetNewGuestName();
            user.Name = guestname;
            user.LoginName = guestname;
            user.Created = DateTime.Now;

            _db.KCUser.Add(user);
            _db.SaveChanges();

            return user;
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
