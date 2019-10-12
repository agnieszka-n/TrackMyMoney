using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Common;
using TrackMyMoney.Models;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.Services.Contracts.Database;

namespace TrackMyMoney.Services
{
    public class CategoriesManager : ICategoriesManager
    {
        private readonly IDatabaseProxy dbProxy;

        public CategoriesManager(IDatabaseProxy dbProxy)
        {
            this.dbProxy = dbProxy;
        }

        public OperationResult<List<CostCategory>> GetCategories()
        {
            using (DbConnection connection = dbProxy.GetConnection())
            {
                try
                {
                    dbProxy.OpenConnection(connection);

                    string query = "select id, name from categories order by name";
                    IQueryResultReader reader = dbProxy.ExecuteReader(query, connection);
                    var result = new List<CostCategory>();

                    while (reader.Read())
                    {
                        var category = new CostCategory
                        {
                            Id = Convert.ToInt32(reader[0]),
                            Name = (string)reader[1]
                        };
                        result.Add(category);
                    }

                    return new OperationResult<List<CostCategory>>(result);
                }
                catch (Exception ex)
                {
                    Logger.LogError(this, ex);
                    return new OperationResult<List<CostCategory>>("An error occurred while getting categories.");
                }
                finally
                {
                    dbProxy.CloseConnection(connection);
                }
            }
        }

        public OperationResult RenameCategory(int id, string newName)
        {
            using (DbConnection connection = dbProxy.GetConnection())
            {
                try
                {
                    dbProxy.OpenConnection(connection);

                    string query = "update categories set name = @name where id = @id";

                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "@id", id },
                        { "@name", newName }
                    };

                    int affectedRowsCount = dbProxy.ExecuteNonQuery(connection, query, parameters);

                    if (affectedRowsCount == 1)
                    {
                        return new OperationResult();
                    }
                    else
                    {
                        throw new Exception("Renaming a category failed.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(this, ex, $"An error occurred while renaming a category with id = [{id}] to [{newName}].");
                    return new OperationResult<List<CostCategory>>("An error occurred while renaming a category.");
                }
                finally
                {
                    dbProxy.CloseConnection(connection);
                }
            }
        }
    }
}
