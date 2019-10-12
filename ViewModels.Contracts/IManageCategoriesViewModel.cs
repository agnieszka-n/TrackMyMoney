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

        RelayCommand GoBackCommand { get; }
        RelayCommand CancelActionCommand { get; }
        RelayCommand ShowRenameCommand { get; }
        RelayCommand SaveRenameCommand { get; }

        ICostCategoryViewModel SelectedCategory { get; set; }
        ObservableCollection<ICostCategoryViewModel> Categories { get; set; }
        ManageCategoriesMenuState MenuState { get; }

        string RenamedCategoryNewName { get; set; }
    }
}
