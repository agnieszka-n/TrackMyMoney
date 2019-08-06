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
using Services;
using Services.Contracts;
using ViewModels;
using ViewModels.Contracts;
using Views;

namespace Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string FILE_NAME = "db.sqlite";
        private readonly string CONNECTION_STRING = $"Data Source={FILE_NAME};Version=3;";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IKernel kernel = new StandardKernel();
            ConfigureKernel(kernel);

            if (!File.Exists(FILE_NAME))
            {
                try
                {
                    SQLiteConnection.CreateFile(FILE_NAME);
                    DatabaseInitializer.Initialize(kernel.Get<SQLiteConnection>());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    return;
                }
            }

            MainWindow = new MainWindow(kernel.Get<ICostsListViewModel>());
            MainWindow.Show();
        }

        private void ConfigureKernel(IKernel kernel)
        {
            kernel.Bind<ICostsListViewModel>().To<CostsListViewModel>();
            kernel.Bind<ICategoriesManager>().To<CategoriesManager>();
            kernel.Bind<SQLiteConnection>().ToMethod(context => new SQLiteConnection(CONNECTION_STRING));
        }
    }
}
