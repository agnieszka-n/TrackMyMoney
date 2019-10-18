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
    public class CategoriesManager : ManagerBase, ICategoriesManager
    {
        public CategoriesManager(IDatabaseProxy dbProxy) : base(dbProxy) { }

        public OperationResult<List<CostCategory>> GetCategories()
        {
            OperationResult<List<CostCategory>> FunctionBody(DbConnection connection)
            {
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

            OperationResult<List<CostCategory>> ErrorHandler(Exception ex)
            {
                Logger.LogError(this, ex);
                return new OperationResult<List<CostCategory>>("An error occurred while getting categories.");
            }

            return ExecuteFunction(FunctionBody, ErrorHandler);
        }

        public OperationResult RenameCategory(int id, string newName)
        {
            OperationResult FunctionBody(DbConnection connection)
            {
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

                throw new Exception("Renaming a category failed.");
            }

            OperationResult ErrorHandler(Exception ex)
            {
                Logger.LogError(this, ex, $"An error occurred while renaming a category with id = [{id}] to [{newName}].");
                return new OperationResult<List<CostCategory>>("An error occurred while renaming a category.");
            }

            return ExecuteFunction(FunctionBody, ErrorHandler);
        }

        public OperationResult AddCategory(string name)
        {
            OperationResult FunctionBody(DbConnection connection)
            {
                string query = "insert into categories(name) values (@name)";

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "@name", name }
                };

                int affectedRowsCount = dbProxy.ExecuteNonQuery(connection, query, parameters);

                if (affectedRowsCount == 1)
                {
                    return new OperationResult();
                }

                throw new Exception("Renaming a category failed.");
            }

            OperationResult ErrorHandler(Exception ex)
            {
                Logger.LogError(this, ex, $"An error occurred while adding a category with name = [{name}].");
                return new OperationResult<List<CostCategory>>("An error occurred while adding a category.");
            }

            return ExecuteFunction(FunctionBody, ErrorHandler);
        }
    }
}
