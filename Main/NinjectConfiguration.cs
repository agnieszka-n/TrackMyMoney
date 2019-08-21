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
using Services.Contracts.Database;
using Services.Database;
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
            kernel.Bind<IDatabaseProxy>().ToMethod(context => new DatabaseProxy(connectionString));
        }
    }
}
