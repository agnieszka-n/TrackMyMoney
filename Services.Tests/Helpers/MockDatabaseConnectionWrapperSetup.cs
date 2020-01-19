using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TrackMyMoney.Common;
using TrackMyMoney.Services.Contracts.Database;

namespace TrackMyMoney.Services.Tests.Helpers
{
    internal static class MockDatabaseConnectionWrapperSetup
    {
        /// <typeparam name="T">Returned OperationResult's parameter type</typeparam>
        public static void SetupConnectionWrapperToExecuteFunction<T>(Mock<IDatabaseProxy> mockDbProxy)
        {
            var mockConnectionWrapper = new Mock<IDatabaseConnectionWrapper>();
            mockConnectionWrapper
                .Setup(x => x.Execute(It.IsAny<Func<SQLiteConnection, OperationResult<T>>>()))
                .Returns((Func<SQLiteConnection, OperationResult<T>> func) => func(null));
            mockDbProxy.Setup(x => x.CreateConnectionWrapper()).Returns(mockConnectionWrapper.Object);
        }

        public static void SetupConnectionWrapperToExecuteAction(Mock<IDatabaseProxy> mockDbProxy)
        {
            var mockConnectionWrapper = new Mock<IDatabaseConnectionWrapper>();
            mockConnectionWrapper
                .Setup(x => x.Execute(It.IsAny<Func<SQLiteConnection, OperationResult>>()))
                .Returns((Func<SQLiteConnection, OperationResult> func) => func(null));
            mockDbProxy.Setup(x => x.CreateConnectionWrapper()).Returns(mockConnectionWrapper.Object);
        }
    }
}
