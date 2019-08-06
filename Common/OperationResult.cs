using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Common
{
    public class OperationResult
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        /// <summary>
        /// A result of a successful action.
        /// </summary>
        protected OperationResult()
        {
            IsSuccess = true;
        }

        /// <summary>
        /// A result of an unsuccessful action with a given error message.
        /// </summary>
        /// <param name="errorMessage">Explanation of an error.</param>
        protected OperationResult(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }

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
