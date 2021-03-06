﻿using System.Data.Common;
﻿using System.Collections.Generic;
using System.Data.SQLite;

namespace TrackMyMoney.Services.Contracts.Database
{
    public interface IDatabaseProxy
    {
        IQueryResultReader ExecuteReader(string query, DbConnection connection);
        int ExecuteNonQuery(DbConnection connection, string query, Dictionary<string, object> queryParameters = null);
        object ExecuteScalar(DbConnection connection, string query, Dictionary<string, object> queryParameters = null);
        IDatabaseConnectionWrapper CreateConnectionWrapper();
    }
}