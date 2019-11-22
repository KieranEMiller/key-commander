using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public KCUserLogin RecordLogin(KCUser user)
        {
            KCUserLogin newLogin = new KCUserLogin();
            newLogin.KCUserLoginId = Guid.NewGuid();
            newLogin.UserId = user.UserId;
            newLogin.Created = DateTime.Now;

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
    }
}
