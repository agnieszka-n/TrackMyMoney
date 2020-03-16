using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TrackMyMoney.Main;
using TrackMyMoney.Services.Contracts.Database;
using TrackMyMoney.Services.Database;

namespace TrackMyMoney.IntegrationTests
{
    public abstract class TextFixtureBase
    {
        protected const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True";
        protected const string LOGGER_FILE_NAME = "TrackMyMoneyTests.log";

        protected void RunTest(Action<IKernel> testMethod)
        {
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

                testMethod(kernel);
            }

            connection.Dispose();
        }
    }
}
