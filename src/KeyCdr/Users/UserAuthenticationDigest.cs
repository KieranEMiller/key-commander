using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Users
{
    public class UserAuthenticationDigest
    {
        private byte[] _hash;
        private byte[] _salt;

        public byte[] Hash { get { return _hash; } set { _hash = value; } }
        public byte[] Salt { get { return _salt; } set { _salt = value; } }

        public string HashBase64
        { get { return Convert.ToBase64String(_hash); } }

        public string SaltBase64
        { get { return Convert.ToBase64String(_salt); } }
    }
}
