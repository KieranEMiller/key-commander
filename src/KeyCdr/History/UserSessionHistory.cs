using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;

namespace KeyCdr.History
{
    public class UserSessionHistory : BaseDBAccess
    {
        public UserSessionHistory()
        { }

        public IList<Data.Session> GetSessionsByUser(Data.KCUser currentUser)
        {
            var results = _db.Sessions
                .Where(s => s.UserId.Equals(currentUser.UserId))
                .Include(s => s.KeySequences)
                .OrderByDescending(s => s.Created);

            return results.ToList();

                    /*
                    (session, user) => new
                    {
                        session.SessionId,
                        user.UserId,
                        session.Created
                    }*/
        }
    }
}
