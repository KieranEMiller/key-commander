using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace KeyCdr.Tests.Integration
{
    [TestFixture]
    public class TestDatabaseEnsurerTests : BaseIntegrationTest
    {
        [TestCase(false, @"metadata=res://*/KeyCdrDataModel.csdl|res://*/KeyCdrDataModel.ssdl|res://*/KeyCdrDataModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost\SPEED_DB;initial catalog=KeyCommander;integrated security=False;user id=keycdr;password=keycdr;multipleactiveresultsets=True;application name=EntityFramework&quot;")]
        public void actual_prod_db_detects_not_test(bool isTestDBConnString, string conn)
        {
            TestDatabaseEnsurer tester = new TestDatabaseEnsurer();
            Assert.That(tester.IsConnStringPointingToTestDB(conn), Is.EqualTo(isTestDBConnString));
        }

        [TestCase(true, @"metadata=res://*/KeyCdrDataModel.csdl|res://*/KeyCdrDataModel.ssdl|res://*/KeyCdrDataModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost\SPEED_DB;initial catalog=KeyCommander_tests;integrated security=False;user id=keycdr_tests;password=keycdr_tests;multipleactiveresultsets=True;application name=EntityFramework&quot;")]
        public void actual_test_db_detects_test(bool isTestDBConnString, string conn)
        {
            TestDatabaseEnsurer tester = new TestDatabaseEnsurer();
            Assert.That(tester.IsConnStringPointingToTestDB(conn), Is.EqualTo(isTestDBConnString));
        }

        [TestCase(false, @"metadata=res://*/KeyCdrDataModel.csdl|res://*/KeyCdrDataModel.ssdl|res://*/KeyCdrDataModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost\SPEED_DB;initial catalog=KeyCommander;integrated security=False;user id=keycdr_tests;password=keycdr_tests;multipleactiveresultsets=True;application name=EntityFramework&quot;")]
        public void test_db_correct_username_wrong_db_name(bool isTestDBConnString, string conn)
        {
            TestDatabaseEnsurer tester = new TestDatabaseEnsurer();
            Assert.That(tester.IsConnStringPointingToTestDB(conn), Is.EqualTo(isTestDBConnString));
        }

        [TestCase(true, @"metadata=res://*/KeyCdrDataModel.csdl|res://*/KeyCdrDataModel.ssdl|res://*/KeyCdrDataModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost\SPEED_DB;initial catalog=KeyCommander_tests;integrated security=False;user id=keycdr;password=keycdr_tests;multipleactiveresultsets=True;application name=EntityFramework&quot;")]
        public void test_db_correct_db_name_wrong_username(bool isTestDBConnString, string conn)
        {
            TestDatabaseEnsurer tester = new TestDatabaseEnsurer();
            Assert.That(tester.IsConnStringPointingToTestDB(conn), Is.EqualTo(isTestDBConnString));
        }

        [Test]
        public void entity_framework_context_from_business_layer_returns_on_testdb()
        {
            TestDatabaseEnsurer tester = new TestDatabaseEnsurer();
            BaseDBAccess dbAccess = new BaseDBAccess();
            Assert.That(tester.IsConnStringPointingToTestDB(dbAccess.Database.Database.Connection.ConnectionString), Is.True);
            Assert.That(tester.IsOnTestDB(), Is.True);
        }
    }
}
