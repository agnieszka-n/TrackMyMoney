using System.Data.Common;
﻿using System.Collections.Generic;
using System.Data.SQLite;

namespace Services.Contracts.Database
{
    public interface IDatabaseProxy
    {
        IQueryResultReader ExecuteReader(string query, DbConnection connection);
        int ExecuteNonQuery(DbConnection connection, string query, Dictionary<string, object> queryParameters = null);
        SQLiteConnection GetConnection();
        void OpenConnection(DbConnection connection);
        void CloseConnection(DbConnection connection);
    }
}