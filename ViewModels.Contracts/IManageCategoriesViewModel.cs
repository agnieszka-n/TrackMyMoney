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
        event Action WentBack;
        event Action Renamed;
        event Action Added;
        event Action Deleted;

        RelayCommand GoBackCommand { get; }
        RelayCommand CancelActionCommand { get; }
        RelayCommand ShowRenameCommand { get; }
        RelayCommand SaveRenameCommand { get; }
        RelayCommand ShowAddCommand { get; }
        RelayCommand SaveAddCommand { get; }
        RelayCommand ShowDeleteCommand { get; }
        RelayCommand ConfirmDeleteCommand { get; }

        ICostCategoryViewModel SelectedCategory { get; set; }
        ObservableCollection<ICostCategoryViewModel> Categories { get; set; }
        ManageCategoriesMenuState MenuState { get; }

        string RenamedCategoryNewName { get; set; }
        string NewCategoryName { get; set; }
    }
}
