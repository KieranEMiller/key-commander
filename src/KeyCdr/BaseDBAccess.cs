using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Data;

namespace KeyCdr
{
    public class BaseDBAccess
    {
        public BaseDBAccess()
        {
            _db = new KeyCommanderEntities1();
            #if DEBUG
                _db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            #endif
        }

        ~BaseDBAccess()
        {
            if (_db != null)
                _db.Dispose();
        }

        protected KeyCommanderEntities1 _db;
    }
}
