using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Common;
using TrackMyMoney.Services.Contracts.Database;

namespace TrackMyMoney.Services
{
    public abstract class ManagerBase
    {
        protected readonly IDatabaseProxy dbProxy;

        protected ManagerBase(IDatabaseProxy dbProxy)
        {
            this.dbProxy = dbProxy;
        }

        protected OperationResult ExecuteFunction(Func<DbConnection, OperationResult> function, Func<Exception, OperationResult> errorHandler)
        {
            return ExecuteFunction<OperationResult>(function, errorHandler);
        }

        protected OperationResult<T> ExecuteFunction<T>(Func<DbConnection, OperationResult<T>> function, Func<Exception, OperationResult<T>> errorHandler)
        {
            return ExecuteFunction<OperationResult<T>>(function, errorHandler);
        }

        private T ExecuteFunction<T>(Func<DbConnection, T> function, Func<Exception, T> errorHandler)
        {
            using (DbConnection connection = dbProxy.GetConnection())
            {
                try
                {
                    dbProxy.OpenConnection(connection);
                    return function(connection);
                }
                catch (Exception ex)
                {
                    return errorHandler(ex);
                }
                finally
                {
                    dbProxy.CloseConnection(connection);
                }
            }
        }
    }
}
