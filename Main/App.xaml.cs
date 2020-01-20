using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TrackMyMoney.Services;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.ViewModels;
using TrackMyMoney.ViewModels.Contracts;
using TrackMyMoney.Views;
using TrackMyMoney.Services.Contracts.Database;
using TrackMyMoney.Common;

namespace TrackMyMoney.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string FILE_NAME = "db.sqlite";
        private const string LOGGER_FILE_NAME = "TrackMyMoney.log";
        private readonly string CONNECTION_STRING = $"Data Source={FILE_NAME};Version=3;";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IKernel kernel = new StandardKernel();
            var diConfiguration = new NinjectConfiguration();
            diConfiguration.ConfigureKernel(kernel, CONNECTION_STRING);

            LoggerInitalizer.InitializeFileLogger(LOGGER_FILE_NAME);

            if (!File.Exists(FILE_NAME))
            {
                try
                {
                    SQLiteConnection.CreateFile(FILE_NAME);

                    using (var connection = kernel.Get<IDatabaseProxy>().CreateConnectionWrapper())
                    {
                        DatabaseInitializer.Initialize(connection);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(this, ex);
                    // TODO implement an error message 
                    Shutdown();
                }
            }

            MainWindow = new MainWindow(kernel.Get<ICostsListViewModel>());
            MainWindow.Show();
        }
    }
}
