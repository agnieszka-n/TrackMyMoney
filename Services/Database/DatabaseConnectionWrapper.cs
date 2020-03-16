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
    public class DatabaseConnectionWrapper : IDatabaseConnectionWrapper
    {
        private readonly bool disposesConnection;
        private readonly SQLiteConnection connection;

        public DatabaseConnectionWrapper(SQLiteConnection connection, bool disposesConnection)
        {
            this.connection = connection;
            this.disposesConnection = disposesConnection;
        }

        public void Execute(Action<SQLiteConnection> action)
        {
            OpenConnection();
            action(connection);
        }

        public T Execute<T>(Func<SQLiteConnection, T> function)
        {
            OpenConnection();
            return function(connection);
        }

        public void Dispose()
        {
            if (disposesConnection)
            {
                connection.Dispose();
            }
        }

        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
    }
}
