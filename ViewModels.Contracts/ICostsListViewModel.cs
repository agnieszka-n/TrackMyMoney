using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface ICostsListViewModel
    {
        ObservableCollection<ICostViewModel> Costs { get; }
        ICostViewModel NewCost { get; }
        bool IsAddingCost { get; }
        ObservableCollection<ICostCategoryViewModel> Categories { get;}
        RelayCommand ShowAddCostCommand { get; }
        RelayCommand CancelAddCostCommand { get; }
        RelayCommand SaveCostCommand { get; }
    }
}
