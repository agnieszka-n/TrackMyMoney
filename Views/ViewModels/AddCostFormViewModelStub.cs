using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.ViewModels
{
    internal class AddCostFormViewModelStub : IAddCostFormViewModel
    {
        public event Action Saved;
        public event Action Cancelled;
        public ObservableCollection<ICostCategoryViewModel> Categories { get; set; }
        public ICostViewModel NewCost { get; }
        public RelayCommand CancelCommand => null;
        public RelayCommand SaveCommand => null;

        public AddCostFormViewModelStub()
        {
            var categories = new List<ICostCategoryViewModel>()
            {
                new CostCategoryViewModelStub() {Name = "Selected category"},
                new CostCategoryViewModelStub() {Name = "Another category"}
            };
            Categories = new ObservableCollection<ICostCategoryViewModel>(categories);
            NewCost = new CostViewModelStub(new DateTime(2000, 12, 1), Categories[0], "Cost subject", 123);
        }
    }
}
