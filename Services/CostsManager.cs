using System;
using System.Collections.Generic;
using System.Data.Common;
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
    public class CostsManager: ICostsManager
    {
        private readonly IDatabaseProxy dbProxy;

        public CostsManager(IDatabaseProxy dbProxy)
        {
            this.dbProxy = dbProxy;
        }

        public OperationResult<List<Cost>> GetCosts()
        {
            using (DbConnection connection = dbProxy.GetConnection())
            {
                try
                {
                    dbProxy.OpenConnection(connection);

                    string query = "select id, date, categoryId, subject, amount from costs";
                    IQueryResultReader reader = dbProxy.ExecuteReader(query, connection);
                    var result = new List<Cost>();

                    while (reader.Read())
                    {
                        var category = new Cost()
                        {
                            Id = Convert.ToInt32(reader[0]),
                            Date = Convert.ToDateTime(reader[1]),
                            CategoryId = Convert.ToInt32(reader[2]),
                            Subject = (string)reader[3],
                            Amount = Convert.ToDecimal(reader[4])
                        };
                        result.Add(category);
                    }

                    return new OperationResult<List<Cost>>(result);
                }
                catch (Exception ex)
                {
                    Logger.LogError(this, ex);
                    return new OperationResult<List<Cost>>("An error occurred while getting costs.");
                }
                finally
                {
                    dbProxy.CloseConnection(connection);
                }
            }
        }

        public OperationResult SaveCost(Cost cost)
        {
            using (DbConnection connection = dbProxy.GetConnection())
            {
                try
                {
                    dbProxy.OpenConnection(connection);

                    string query = "insert into costs(date, categoryId, subject, amount)" +
                                   " values(@date, @categoryId, @subject, @amount)";

                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "@date", cost.Date.Date },
                        { "@categoryId", cost.CategoryId },
                        { "@subject", cost.Subject },
                        { "@amount", cost.Amount }
                    };

                    int affectedRowsCount = dbProxy.ExecuteNonQuery(connection, query, parameters);

                    if (affectedRowsCount == 1)
                    {
                        return new OperationResult();
                    }
                    else
                    {
                        throw new Exception("Inserting a new row failed.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(this, ex);
                    return new OperationResult("An error occurred while saving a cost.");
                }
                finally
                {
                    dbProxy.CloseConnection(connection);
                }
            }
        }
    }
}
