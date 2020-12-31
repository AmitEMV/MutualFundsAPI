using MutualFundsAPI.Interfaces;
using MySqlConnector;
using System;

namespace MutualFundsAPI.Helpers
{
    public class AppDb : IDisposable, IDBConnector
    {
        public MySqlConnection Connection { get; set; }

        public AppDb(string connectionString)
        {
            CreateDBConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();

        public void CreateDBConnection(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
    }
}
