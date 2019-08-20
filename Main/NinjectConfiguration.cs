using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Parameters;
using Services;
using Services.Contracts;
using ViewModels;
using ViewModels.Contracts;

namespace Main
{
    internal class NinjectConfiguration
    {
        public void ConfigureKernel(IKernel kernel, string connectionString)
        {
            kernel.Bind<ICostsListViewModel>().To<CostsListViewModel>();
            kernel.Bind<ICategoriesManager>().To<CategoriesManager>();
            kernel.Bind<SQLiteConnection>().ToMethod(context => new SQLiteConnection(connectionString));
        }
    }
}
