using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Users
{
    public class UserAuthenticationSecurity
    {
        public const int SALT_SIZE_IN_BYTES = 64;
        public const int HASH_SIZE_IN_BYTES = 64;

        public bool IsValid(UserAuthenticationDigest digest, string password)
        {
            var newDigest = Hash(password, digest.Salt);
            return newDigest.HashBase64.Equals(digest.HashBase64);
        }

        public byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SALT_SIZE_IN_BYTES];
                saltGenerator.GetBytes(salt);
                return (salt);
            }
        }

        public UserAuthenticationDigest Hash(string password)
        {
            return Hash(password, this.GenerateSalt());
        }

        public UserAuthenticationDigest Hash(string password, byte[] salt)
        {
            var digest = new UserAuthenticationDigest();
            digest.Salt = salt;
            digest.Hash = this.HashWithSalt(password, digest.Salt);
            return digest;
        }

        public byte[] HashWithSalt(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt))
            {
                hashGenerator.IterationCount = 512;
                byte[] hash = hashGenerator.GetBytes(HASH_SIZE_IN_BYTES);
                return hash;
            }
        }
    }
}
