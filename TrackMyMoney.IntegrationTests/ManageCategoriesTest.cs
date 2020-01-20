using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;
using TrackMyMoney.Main;
using TrackMyMoney.Services.Contracts.Database;
using TrackMyMoney.Services.Database;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.IntegrationTests
{
    [TestFixture]
    public class ManageCategoriesTest
    {
        [Test]
        public void Can_Add_Category()
        {
            // Arrange
            const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True";
            const string LOGGER_FILE_NAME = "TrackMyMoneyTests.log";

            IKernel kernel = new StandardKernel();
            var diConfiguration = new NinjectConfiguration();
            diConfiguration.ConfigureKernel(kernel, CONNECTION_STRING);

            var connection = new SQLiteConnection(CONNECTION_STRING);
            var dbProxy = new DatabaseProxy(connection);
            kernel.Rebind<IDatabaseProxy>().ToConstant(dbProxy);

            LoggerInitalizer.InitializeFileLogger(LOGGER_FILE_NAME);

            using (var connectionWrapper = dbProxy.CreateConnectionWrapper())
            {
                DatabaseInitializer.Initialize(connectionWrapper);

                // Act
                var mainVm = kernel.Get<ICostsListViewModel>();
                mainVm.ShowManageCategoriesCommand.Execute(null);

                var manageCategoriesVm = mainVm.ManageCategoriesViewModel;
                manageCategoriesVm.ShowAddCommand.Execute(null);
                manageCategoriesVm.NewCategoryName = "New category";
                manageCategoriesVm.SaveAddCommand.Execute(null);

                // Assert
                var categories = manageCategoriesVm.Categories;
                Assert.AreEqual(1, categories.Count);
                Assert.AreEqual("New category", categories.First().Name);
            }

            connection.Dispose();
        }
    }
}
