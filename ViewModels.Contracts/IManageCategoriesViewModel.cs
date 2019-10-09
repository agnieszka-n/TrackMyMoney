using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface IManageCategoriesViewModel
    {
        event Action Cancelled;

        RelayCommand CancelCommand { get; }
        ICostCategoryViewModel SelectedCategory { get; set; }
        ObservableCollection<ICostCategoryViewModel> Categories { get; set; }
    }
}
