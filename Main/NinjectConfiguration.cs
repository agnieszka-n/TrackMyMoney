using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Parameters;
using TrackMyMoney.Services;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.Services.Contracts.Database;
using TrackMyMoney.Services.Database;
using TrackMyMoney.ViewModels;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Main
{
    internal class NinjectConfiguration
    {
        public void ConfigureKernel(IKernel kernel, string connectionString)
        {
            kernel.Bind<ICostsListViewModel>().To<CostsListViewModel>();
            kernel.Bind<IAddCostFormViewModel>().To<AddCostFormViewModel>();
            kernel.Bind<ICategoriesManager>().To<CategoriesManager>();
            kernel.Bind<ICostsManager>().To<CostsManager>();
            kernel.Bind<IDatabaseProxy>().ToMethod(context => new DatabaseProxy(connectionString));
        }
    }
}
