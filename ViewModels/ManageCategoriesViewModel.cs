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
        public event Action Cancelled;

        private ICostCategoryViewModel selectedCategory;
        public RelayCommand CancelCommand { get; }

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
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Cancel()
        {
            SelectedCategory = null;
            Cancelled?.Invoke();
        }
    }
}
