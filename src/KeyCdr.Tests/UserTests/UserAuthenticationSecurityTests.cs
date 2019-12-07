using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyCdr.Users;
using NUnit.Framework;

namespace KeyCdr.Tests.UserTests
{
    [TestFixture]
    public class UserAuthenticationSecurityTests
    {
        [Test]
        public void salt_is_unique_across_50_gens()
        {
            UserAuthenticationSecurity sec = new UserAuthenticationSecurity();

            List<string> prevSalts = new List<string>();
            for(int i=0;i < 50; i ++)
            {
                string salt = Convert.ToBase64String(sec.GenerateSalt());
                Assert.That(salt, Is.Not.SubsetOf(prevSalts));
                prevSalts.Add(salt);
            }
        }

        [Test]
        public void hashes_match_with_same_salt_and_password()
        {
            UserAuthenticationSecurity sec = new UserAuthenticationSecurity();
            var digest = sec.Hash("asdfasdf");

            bool isValid = sec.IsValid(digest, "asdfasdf");
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void hashes_does_not_match_with_same_salt_and_different_password()
        {
            UserAuthenticationSecurity sec = new UserAuthenticationSecurity();
            var digest = sec.Hash("asdfasdf");

            bool isValid = sec.IsValid(digest, "asdfas1f");
            Assert.That(isValid, Is.False);
        }
    }
}
