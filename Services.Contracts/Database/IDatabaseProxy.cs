using System.Data.Common;
﻿using System.Collections.Generic;
using System.Data.SQLite;

namespace TrackMyMoney.Services.Contracts.Database
{
    /// <summary>
    /// Performs database operations using a connection injected into each method.
    /// </summary>
    public interface IDatabaseProxy
    {
        IQueryResultReader ExecuteReader(DbConnection connection, string query);
        int ExecuteNonQuery(DbConnection connection, string query, Dictionary<string, object> queryParameters = null);
        object ExecuteScalar(DbConnection connection, string query, Dictionary<string, object> queryParameters = null);
        IDatabaseConnectionWrapper CreateConnectionWrapper();
    }
}