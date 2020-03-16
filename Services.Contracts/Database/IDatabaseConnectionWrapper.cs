using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.Services.Contracts.Database
{
    /// <summary>
    /// Wraps a connection to dispose it after a real query or keep after a query in tests.
    /// </summary>
    public interface IDatabaseConnectionWrapper : IDisposable
    {
       void Execute(Action<SQLiteConnection> action);
        T Execute<T>(Func<SQLiteConnection, T> function);
    }
}
