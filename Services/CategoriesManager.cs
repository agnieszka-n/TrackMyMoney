using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;
using Services.Contracts;

namespace Services
{
    public class CategoriesManager : ICategoriesManager
    {
        private readonly SQLiteConnection connection;

        public CategoriesManager(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        public OperationResult<List<CostCategory>> GetCategories()
        {
            try
            {
                connection.Open();

                string query = "select id, name from categories";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                var reader = command.ExecuteReader();
                var result = new List<CostCategory>();

                while (reader.Read())
                {
                    var category = new CostCategory
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                    result.Add(category);
                }

                connection.Close();
                return new OperationResult<List<CostCategory>>(result);
            }
            catch (Exception e)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }

                Trace.WriteLine(e);
                return new OperationResult<List<CostCategory>>("An error occurred while getting categories.");
            }
        }
    }
}
