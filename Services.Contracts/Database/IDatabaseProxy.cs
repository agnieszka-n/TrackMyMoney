using System.Data.Common;

namespace Services.Contracts.Database
{
    public interface IDatabaseProxy
    {
        IQueryResultReader ExecuteReader(string query, DbConnection connection);
        DbConnection GetConnection();
        void OpenConnection(DbConnection connection);
        void CloseConnection(DbConnection connection);
    }
}