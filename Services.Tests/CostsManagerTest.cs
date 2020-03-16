using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Common;
using TrackMyMoney.Models;
using Moq;
using NUnit.Framework;
using TrackMyMoney.Services.Contracts.Database;
using TrackMyMoney.Services.Tests.Helpers;

namespace TrackMyMoney.Services.Tests
{
    [TestFixture]
    public class CostsManagerTest
    {
        [Test]
        public void Can_Get_Costs()
        {
            // Arrange
            var mockDbProxy = new Mock<IDatabaseProxy>();

            object[] values =
            {
                1, "2000-01-05", 111, "Pasta", 12
            };
            var reader = new QueryResultReaderStub(5, values);
            mockDbProxy.Setup(x => x.ExecuteReader(It.IsAny<DbConnection>(), It.IsAny<string>())).Returns(reader);
            MockDatabaseConnectionWrapperSetup.SetupConnectionWrapperExecuteFunction<List<Cost>>(mockDbProxy);

            var manager = new CostsManager(mockDbProxy.Object);

            // Act
            OperationResult<List<Cost>> result = manager.GetCosts();

            // Assert
            Assert.AreEqual(true, result.IsSuccess, "Should return success from a database.");
            Assert.AreEqual(1, result.Data.Count, "Should return 1 row from a database.");

            var cost = result.Data[0];

            Assert.AreEqual(1, cost.Id, "Invalid cost id.");
            Assert.AreEqual(new DateTime(2000, 1, 5), cost.Date, "Invalid cost date.");
            Assert.AreEqual(111, cost.CategoryId, "Invalid cost category id.");
            Assert.AreEqual("Pasta", cost.Subject, "Invalid cost subject.");
            Assert.AreEqual(12, cost.Amount, "Invalid cost amount.");
        }

        [Test]
        public void Can_Save_Cost()
        {
            // Arrange
            var mockDbProxy = new Mock<IDatabaseProxy>();
            mockDbProxy.Setup(x => x.ExecuteNonQuery(It.IsAny<DbConnection>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>())).Returns(1);
            MockDatabaseConnectionWrapperSetup.SetupConnectionWrapperExecuteFunction(mockDbProxy);

            var manager = new CostsManager(mockDbProxy.Object);

            // Act
            OperationResult result = manager.AddCost(new Cost());

            // Assert
            Assert.AreEqual(true, result.IsSuccess, "Should return success from a database.");
        }
    }
}
