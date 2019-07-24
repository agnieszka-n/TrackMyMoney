using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    static class DatabaseInitializer
    {
        public static void Initialize(SQLiteConnection connection)
        {
            string[] queries = GetQueries();

            connection.Open();
            ExecuteNonQueryCommand(connection, " BEGIN TRANSACTION ");

            foreach (var query in queries)
            {
                try
                {
                    ExecuteNonQueryCommand(connection, query);
                }
                catch (Exception)
                {
                    ExecuteNonQueryCommand(connection, " ROLLBACK TRANSACTION ");
                    throw;
                }
            }

            ExecuteNonQueryCommand(connection, " COMMIT TRANSACTION ");
            connection.Close();
        }

        private static string[] GetQueries()
        {
            string createCategoriesQuery = " CREATE TABLE categories ( " +
                                           " id INTEGER PRIMARY KEY " +
                                           " , name TEXT UNIQUE ) " +
                                           " ; ";
            string createCostsQuery = " CREATE TABLE costs " +
                                      " ( id INTEGER PRIMARY KEY " +
                                      " , date DATE " +
                                      " , categoryId INTEGER CONSTRAINT costsCategoriesFk REFERENCES categories(id) " +
                                      " , subject TEXT " +
                                      " , amount DECIMAL(10,2) ) " +
                                      " ; ";
            string createCostsDateIndexQuery = " CREATE INDEX costsDateIdx ON costs(date) ";
            string createCostsSubjectIndexQuery = " CREATE INDEX costsSubjectIdx ON costs(subject) ";

            return new[] { createCategoriesQuery, createCostsQuery, createCostsDateIndexQuery, createCostsSubjectIndexQuery };
        }

        private static void ExecuteNonQueryCommand(SQLiteConnection connection, string sql)
        {
            var command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
