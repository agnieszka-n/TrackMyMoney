using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace ViewModels.Contracts
{
    public interface ICostsListViewModel
    {
        ObservableCollection<ICostViewModel> Costs { get; }
        ICostViewModel NewCost { get; }
        bool IsAddingCost { get; }
        ObservableCollection<ICostCategoryViewModel> Categories { get;}
        RelayCommand AddCostCommand { get; }
        RelayCommand CancelAddingCommand { get; }
        RelayCommand SaveCostCommand { get; }
    }
}
