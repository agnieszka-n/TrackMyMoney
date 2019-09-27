using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using TrackMyMoney.Models;
using TrackMyMoney.ViewModels;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.ViewModels
{
    internal class AddCostFormViewModelStub: IAddCostFormViewModel
    {
        public event Action CostSaved;
        public event Action CostCancelled;
        public ObservableCollection<ICostCategoryViewModel> Categories { get; set; }
        public ICostViewModel NewCost { get; }
        public RelayCommand CancelCommand => null;
        public RelayCommand SaveCommand => null;

        public AddCostFormViewModelStub()
        {
            var categories = new List<ICostCategoryViewModel>()
            {
                new CostCategoryViewModel(new CostCategory() {Id = 1, Name = "Selected category"}),
                new CostCategoryViewModel(new CostCategory() {Id = 1, Name = "Another category"})
            };
            Categories = new ObservableCollection<ICostCategoryViewModel>(categories);
            NewCost = new CostViewModelStub(new DateTime(2000,12,1), Categories[0], "Cost subject", 123);
        }
    }
}
