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
        private readonly Func<SQLiteConnection> getConnection;
        private readonly bool disposesConnection;

        public DatabaseProxy(SQLiteConnection connection)
        {
            getConnection = () => connection;
            disposesConnection = false;
        }

        public DatabaseProxy(string connectionString)
        {
            getConnection = () => new SQLiteConnection(connectionString);
            disposesConnection = true;
        }

        public IDatabaseConnectionWrapper CreateConnectionWrapper()
        {
            var connection = getConnection();
            return new DatabaseConnectionWrapper(connection, disposesConnection);
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
            AddParametersToCommand(command, queryParameters);

            return command.ExecuteNonQuery();
        }

        public object ExecuteScalar(DbConnection connection, string query, Dictionary<string, object> queryParameters = null)
        {
            DbCommand command = connection.CreateCommand();
            command.CommandText = query;
            AddParametersToCommand(command, queryParameters);

            return command.ExecuteScalar();
        }

        private static void AddParametersToCommand(DbCommand command, Dictionary<string, object> queryParameters)
        {
            if (queryParameters != null)
            {
                foreach (var parameter in queryParameters)
                {
                    command.Parameters.Add(new SQLiteParameter() { ParameterName = parameter.Key, Value = parameter.Value });
                }
            }
        }
    }
}
