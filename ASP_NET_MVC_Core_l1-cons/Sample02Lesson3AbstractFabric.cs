using Oracle.ManagedDataAccess.Client;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Sample02Lesson3AbstractFabric
    {
        static void Main(string[] args)
        {
            
            LogSaver oracleLogSaver = new LogSaver(new OracleClientFactory());
            oracleLogSaver.Save(new LogEntry[] { new LogEntry(), new LogEntry()});

            LogSaver sqliteLogSaver = new LogSaver(SqliteFactory.Instance);
            sqliteLogSaver.Save(new LogEntry[] { new LogEntry(), new LogEntry() });
        }
    }

    public class LogEntry
    {
        public string Text { get; set; }
    }

    public class LogSaver
    {
        private readonly DbProviderFactory _factory;
        public LogSaver (DbProviderFactory factory)
        {
            _factory = factory;
        }

        public void Save(IEnumerable<LogEntry> logs)
        {
            using (var dbConnection = _factory.CreateConnection())
            {
                using (var dbCommand = _factory.CreateCommand())
                {
                    SetCommandArguments(logs);
                    dbCommand.ExecuteNonQuery();
                }
            }
        }

        private void SetConnectioString (DbConnection connection)
        {
            // TODO: set db connection
        }

        private void SetCommandArguments(IEnumerable<LogEntry> logs)
        {
            // TODO: set db connection
        }
    }
}
