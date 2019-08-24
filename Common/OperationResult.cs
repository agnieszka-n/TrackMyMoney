using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Common
{
    /// <summary>
    /// Represents a result of an operation related to a database.
    /// </summary>
    public class OperationResult
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        /// <summary>
        /// A result of a successful action.
        /// </summary>
        public OperationResult()
        {
            IsSuccess = true;
        }

        /// <summary>
        /// A result of an unsuccessful action with a given error message.
        /// </summary>
        /// <param name="errorMessage">Explanation of an error.</param>
        public OperationResult(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Represents a result of an operation related to a database. Contains data returned from it.
    /// </summary>
    public class OperationResult<TData> : OperationResult
    {
        public TData Data { get; }

        /// <summary>
        /// A result of a successful action.
        /// </summary>
        public OperationResult(TData data): base()
        {
            Data = data;
        }

        /// <summary>
        /// A result of an unsuccessful action with a given error message.
        /// </summary>
        /// <param name="errorMessage">Explanation of an error.</param>
        public OperationResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}
