using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.Database;

namespace Services.Database
{
    public class DatabaseProxy : IDatabaseProxy
    {
        private readonly string connectionString;

        public DatabaseProxy(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public void OpenConnection(DbConnection connection)
        {
            connection.Open();
        }

        public void CloseConnection(DbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public IQueryResultReader ExecuteReader(string query, DbConnection connection)
        {
            DbCommand command = connection.CreateCommand();
            command.CommandText = query;

            var reader = command.ExecuteReader();
            return new QueryResultReader(reader);
        }
    }
}
