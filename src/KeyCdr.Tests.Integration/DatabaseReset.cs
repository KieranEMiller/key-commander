using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KeyCdr.Tests.Integration
{
    public class DatabaseReset
    {
        public void Reset()
        {
            Data.KeyCdrDataEntities db = new Data.KeyCdrDataEntities();

            TestDatabaseEnsurer ensurer = new TestDatabaseEnsurer();
            if (!ensurer.IsEntityFrameworkPointingToTest(db))
                throw new Exception("not configured or pointing to the test environment...yikes!");

            db.Database.ExecuteSqlCommand(GetClearCmd("KCUserLogin"));
            db.Database.ExecuteSqlCommand(GetClearCmd("AnalysisAccuracy"));
            db.Database.ExecuteSqlCommand(GetClearCmd("AnalysisSpeed"));
            db.Database.ExecuteSqlCommand(GetClearCmd("KeySequenceAnalysis"));
            db.Database.ExecuteSqlCommand(GetClearCmd("KeySequence"));
            db.Database.ExecuteSqlCommand(GetClearCmd("Session"));
            db.Database.ExecuteSqlCommand(GetClearCmd("KCUser"));
        }

        public string GetClearCmd(string table)
        {
            return string.Format("TRUNCATE TABLE {0}", table);
        }
    }
}
