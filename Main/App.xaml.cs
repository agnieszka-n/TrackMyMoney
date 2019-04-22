using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;
using ViewModelsContracts;
using Views;

namespace Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IKernel kernel = new StandardKernel();
            ConfigureKernel(kernel);

            MainWindow = new MainWindow(kernel.Get<ICostsListViewModel>());
            MainWindow.Show();
        }

        private void ConfigureKernel(IKernel kernel)
        {
            kernel.Bind<ICostsListViewModel>().To<CostsListViewModel>();
        }
    }
}
