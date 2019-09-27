using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface ICostsListViewModel
    {
        IAddCostFormViewModel AddCostFormViewModel { get; }
        ObservableCollection<ICostViewModel> Costs { get; }
        bool IsAddingCost { get; }
        ObservableCollection<ICostCategoryViewModel> Categories { get;}
        RelayCommand ShowAddCostCommand { get; }
    }
}
