﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;
using Moq;
using NUnit.Framework;
using Services.Contracts.Database;

namespace Services.Tests
{
    [TestFixture]
    public class CategoriesManagerTest
    {
        [Test]
        public void Can_Get_Categories()
        {
            // Arrange
            var mockDbProxy = new Mock<IDatabaseProxy>();
            object[] values =
            {
                1, "name 1",
                2, "name 2"
            };
            var reader = new QueryResultReaderStub(2, values);
            mockDbProxy.Setup(x => x.ExecuteReader(It.IsAny<string>(), It.IsAny<DbConnection>())).Returns(reader);

            var manager = new CategoriesManager(mockDbProxy.Object);

            // Act
            OperationResult<List<CostCategory>> result = manager.GetCategories();

            // Assert
            Assert.AreEqual(true, result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual(1, result.Data[0].Id);
            Assert.AreEqual("name 1", result.Data[0].Name);
        }
    }
}