using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.ViewModels;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.ViewModels
{
    class ManageCategoriesViewModelStub : IManageCategoriesViewModel
    {
        public event Action WentBack;
        public event Action Renamed;
        public event Action Added;
        public event Action Deleted;

        public RelayCommand GoBackCommand => null;
        public RelayCommand ShowRenameCommand => null;
        public RelayCommand SaveRenameCommand => null;
        public RelayCommand ShowAddCommand => null;
        public RelayCommand SaveAddCommand => null;
        public RelayCommand ShowDeleteCommand => null;
        public RelayCommand ConfirmDeleteCommand => null;
        public RelayCommand CancelActionCommand => null;

        public ICostCategoryViewModel SelectedCategory { get; set; }
        public ObservableCollection<ICostCategoryViewModel> Categories { get; set; }
        public ManageCategoriesMenuState MenuState { get; }
        public string RenamedCategoryNewName { get; set; }
        public string NewCategoryName { get; set; }

        public ManageCategoriesViewModelStub()
        {
            var categories = new List<ICostCategoryViewModel>()
            {
                new CostCategoryViewModelStub() { Name = "Selected category"},
                new CostCategoryViewModelStub() { Name = "Another category"}
            };

            MenuState = ManageCategoriesMenuState.DELETE;
            Categories = new ObservableCollection<ICostCategoryViewModel>(categories);
            SelectedCategory = Categories.First();
            RenamedCategoryNewName = "Renamed category";
            NewCategoryName = "New category";

        }
    }
}
