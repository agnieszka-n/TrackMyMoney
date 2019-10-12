using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels
{
    public class ManageCategoriesViewModel : ViewModelBase, IManageCategoriesViewModel
    {
        public event Action WentBack;
        public event Action Renamed;

        public RelayCommand GoBackCommand { get; }
        public RelayCommand ShowRenameCommand { get; }
        public RelayCommand SaveRenameCommand { get; }
        public RelayCommand CancelActionCommand { get; }

        private readonly ICategoriesManager categoriesManager;

        private ICostCategoryViewModel selectedCategory;
        public ICostCategoryViewModel SelectedCategory
        {
            get => selectedCategory;
            set { Set(() => SelectedCategory, ref selectedCategory, value); }
        }

        private ObservableCollection<ICostCategoryViewModel> categories;
        public ObservableCollection<ICostCategoryViewModel> Categories
        {
            get => categories;
            set
            {
                // Preserve the selected category after renaming.
                var selectedCategoryId = SelectedCategory?.Id;

                if (Set(() => Categories, ref categories, value))
                {
                    SelectedCategory = Categories?.FirstOrDefault(x => x.Id == selectedCategoryId);
                }
            }
        }

        private ManageCategoriesMenuState menuState;
        public ManageCategoriesMenuState MenuState
        {
            get => menuState;
            private set
            {
                Set(() => MenuState, ref menuState, value);
            }
        }

        private string renamedCategoryNewName;
        public string RenamedCategoryNewName
        {
            get => renamedCategoryNewName;
            set
            {
                Set(() => RenamedCategoryNewName, ref renamedCategoryNewName, value);
            }
        }

        public ManageCategoriesViewModel(ICategoriesManager categoriesManager)
        {
            this.categoriesManager = categoriesManager;
            GoBackCommand = new RelayCommand(GoBack);
            SaveRenameCommand = new RelayCommand(SaveRename);
            ShowRenameCommand = new RelayCommand(ShowRename);
            CancelActionCommand = new RelayCommand(CancelAction);
            MenuState = ManageCategoriesMenuState.DEFAULT;
        }

        private void CancelAction()
        {
            MenuState = ManageCategoriesMenuState.DEFAULT;
        }

        private void ShowRename()
        {
            MenuState = ManageCategoriesMenuState.RENAME;
        }

        private void SaveRename()
        {
            var result = categoriesManager.RenameCategory(SelectedCategory.Id, RenamedCategoryNewName);

            if (result.IsSuccess)
            {
                RenamedCategoryNewName = null;
                CancelAction();
                Renamed?.Invoke();
            }

            // TODO implement an error message
        }

        private void GoBack()
        {
            SelectedCategory = null;
            WentBack?.Invoke();
        }
    }
}
