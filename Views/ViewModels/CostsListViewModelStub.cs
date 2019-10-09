using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Common;
using TrackMyMoney.Models;
using TrackMyMoney.ViewModels;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.ViewModels
{
    internal class CostsListViewModelStub : ICostsListViewModel
    {
        public IAddCostFormViewModel AddCostFormViewModel { get; }
        public IManageCategoriesViewModel ManageCategoriesViewModel { get; }
        public ObservableCollection<ICostViewModel> Costs { get; set; }
        public CostsListMenuState MenuState { get; }
        public ObservableCollection<ICostCategoryViewModel> Categories { get; }
        public RelayCommand ShowAddCostCommand => null;
        public RelayCommand ShowManageCategoriesCommand => null;

        public CostsListViewModelStub()
        {
            AddCostFormViewModel = new AddCostFormViewModelStub();
            ManageCategoriesViewModel = new ManageCategoriesViewModelStub();

            MenuState = CostsListMenuState.DEFAULT;
            Categories = new ObservableCollection<ICostCategoryViewModel>
            {
                new CostCategoryViewModelStub() { Name = "Some category"},
                new CostCategoryViewModelStub() { Name = "Another category"}
            };


            Costs = new ObservableCollection<ICostViewModel>
            {
                new CostViewModelStub(new DateTime(2000, 1, 1), Categories[0], "Pasta", 10),
                new CostViewModelStub(new DateTime(2000, 1, 1), Categories[1], "Pizza", 15),
                new CostViewModelStub(new DateTime(2000, 1, 22), Categories[1], "Burger", 20),
            };
        }
    }
}
