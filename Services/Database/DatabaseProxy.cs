using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Services.Contracts.Database;

namespace TrackMyMoney.Services.Database
{
    public class DatabaseProxy : IDatabaseProxy
    {
        private readonly string connectionString;

        public DatabaseProxy(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SQLiteConnection GetConnection()
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

        public int ExecuteNonQuery(DbConnection connection, string query, Dictionary<string, object> queryParameters = null)
        {
            DbCommand command = connection.CreateCommand();
            command.CommandText = query;

            if (queryParameters != null)
            {
                foreach (var parameter in queryParameters)
                {
                    command.Parameters.Add(new SQLiteParameter() { ParameterName = parameter.Key, Value = parameter.Value });
                }
            }

            return command.ExecuteNonQuery();
        }
    }
}
