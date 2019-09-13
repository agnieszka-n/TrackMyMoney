using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;
using Services.Contracts;
using Services.Contracts.Database;

namespace Services
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

                    string query = "select id, name from categories";
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
    }
}
