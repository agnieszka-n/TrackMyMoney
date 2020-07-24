using System.Windows;
using Ninject;
using TrackMyMoney.Services;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.Services.Contracts.Database;
using TrackMyMoney.Services.Contracts.Dialogs;
using TrackMyMoney.Services.Database;
using TrackMyMoney.Services.Dialogs;
using TrackMyMoney.ViewModels;
using TrackMyMoney.ViewModels.Contracts;
using TrackMyMoney.Views;
using IDialogService = TrackMyMoney.Services.Contracts.Dialogs.IDialogService;

namespace TrackMyMoney.Main
{
    public class NinjectConfiguration
    {
        public void ConfigureKernel(IKernel kernel, string connectionString)
        {
            kernel.Bind<ICostsListViewModel>().To<CostsListViewModel>();
            kernel.Bind<IAddCostFormViewModel>().To<AddCostFormViewModel>();
            kernel.Bind<IManageCategoriesViewModel>().To<ManageCategoriesViewModel>();

            kernel.Bind<ICategoriesManager>().To<CategoriesManager>();
            kernel.Bind<ICostsManager>().To<CostsManager>();

            kernel.Bind<IDatabaseProxy>().ToMethod(context => new DatabaseProxy(connectionString));

            kernel.Bind<IDialogService>().ToMethod(context => new DialogService(() => kernel.Get<IOkCancelDialogWindow>()));
            kernel.Bind<IOkCancelDialogWindow>().ToMethod(context => new OkCancelDialogWindow(Application.Current.MainWindow));
        }
    }
}
