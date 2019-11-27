using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KeyCdr.Tests.Integration
{
    [TestFixture]
    public class BaseIntegrationTest
    {
        [SetUp]
        public void base_setup()
        {
            TestDatabaseEnsurer insurance = new TestDatabaseEnsurer();
            Assert.That(insurance.IsOnTestDB(), Is.True);

            DatabaseReset reset = new DatabaseReset();
            reset.Reset();
        }
    }
}
