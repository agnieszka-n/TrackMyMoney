using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.Services.Contracts.Database
{
    public interface IDatabaseConnectionWrapper : IDisposable
    {
       void Execute(Action<SQLiteConnection> function);
        T Execute<T>(Func<SQLiteConnection, T> function);
    }
}
