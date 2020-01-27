using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using TrackMyMoney.Common;
using TrackMyMoney.Services.Contracts.Messages;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface ICostsListViewModel
    {
        IMessagesService MessagesService { get; }

        IAddCostFormViewModel AddCostFormViewModel { get; }
        IManageCategoriesViewModel ManageCategoriesViewModel { get; }

        ObservableCollection<ICostViewModel> Costs { get; }
        CostsListMenuState MenuState { get; }
        ObservableCollection<ICostCategoryViewModel> Categories { get;}

        RelayCommand ShowAddCostCommand { get; }
        RelayCommand ShowManageCategoriesCommand { get; }
    }
}
