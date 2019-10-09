using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels
{
    public class ManageCategoriesViewModel : ViewModelBase, IManageCategoriesViewModel
    {
        public event Action WentBack;

        public RelayCommand GoBackCommand { get; }

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
                Set(() => Categories, ref categories, value);
            }
        }

        public ManageCategoriesViewModel()
        {
            GoBackCommand = new RelayCommand(GoBack);
        }

        private void GoBack()
        {
            SelectedCategory = null;
            WentBack?.Invoke();
        }
    }
}
