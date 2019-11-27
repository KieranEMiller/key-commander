using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Tests.Integration
{
    public class TestDatabaseEnsurer
    {
        private const string TEST_DB_CONN_STRING_KEY = "KeyCdrDataEntities";
        private const string TEST_DB_NAME = "KeyCommander_tests";
        private const string TEST_DB_USER = "keycdr_tests";

        public bool IsConnStringPointingToTestDB(string connString)
        {
            if (!connString.Contains(TEST_DB_NAME)
                || !connString.Contains(TEST_DB_USER))
                return false;

            return true;
        }

        public virtual string GetAppConfigConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[TEST_DB_CONN_STRING_KEY].ConnectionString;
        }

        public bool IsEntityFrameworkPointingToTest(Data.KeyCdrDataEntities db)
        {
            //test the entity framework database
            if (!db.Database.Connection.Database.Equals(TEST_DB_NAME))
                return false;

            //test the entity framework connection string
            if (!IsConnStringPointingToTestDB(db.Database.Connection.ConnectionString))
                return false;

            return true;
        }

        public bool IsOnTestDB()
        {
            //test connection string
            string connectionString = GetAppConfigConnectionString();
            if (!IsConnStringPointingToTestDB(connectionString))
                return false;

            Data.KeyCdrDataEntities db = new Data.KeyCdrDataEntities();
            if (!IsEntityFrameworkPointingToTest(db))
                return false;

            return true;
        }
    }
}
