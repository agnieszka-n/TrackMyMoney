using System.Data.Common;
﻿using System.Collections.Generic;

namespace Services.Contracts.Database
{
    public interface IDatabaseProxy
    {
        IQueryResultReader ExecuteReader(string query, DbConnection connection);
        int ExecuteNonQuery(DbConnection connection, string query, Dictionary<string, object> queryParameters = null);
        DbConnection GetConnection();
        void OpenConnection(DbConnection connection);
        void CloseConnection(DbConnection connection);
    }
}