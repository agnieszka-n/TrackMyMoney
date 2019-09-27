using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface IAddCostFormViewModel
    {
        event Action CostSaved;
        event Action CostCancelled;
        ObservableCollection<ICostCategoryViewModel> Categories { get; set; }
        ICostViewModel NewCost { get; }
        RelayCommand CancelCommand { get; }
        RelayCommand SaveCommand { get; }
    }
}
