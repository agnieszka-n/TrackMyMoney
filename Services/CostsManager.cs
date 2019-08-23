using System;
using System.Collections.Generic;
using System.Data.Common;
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
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                    return new OperationResult<List<Cost>>("An error occurred while getting costs.");
                }
                finally
                {
                    dbProxy.CloseConnection(connection);
                }
            }

        }
    }
}
