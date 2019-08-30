using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Models;
using ViewModels;
using ViewModels.Contracts;

namespace Views.ViewModels
{
    internal class CostsListViewModelStub : ICostsListViewModel
    {
        public ObservableCollection<ICostViewModel> Costs { get; set; }
        public ICostViewModel NewCost { get; }
        public bool IsAddingCost { get; }
        public ObservableCollection<ICostCategoryViewModel> Categories { get; }
        public RelayCommand ShowAddCostCommand => null;
        public RelayCommand CancelAddingCommand => null;
        public RelayCommand SaveCostCommand => null;

        public CostsListViewModelStub()
        {
            IsAddingCost = true;
            Categories = new ObservableCollection<ICostCategoryViewModel>
            {
                new CostCategoryViewModel(new CostCategory() { Id = 1, Name = "Selected category"}),
                new CostCategoryViewModel(new CostCategory() { Id = 2, Name = "Another category"})
            };

            NewCost = new CostViewModelStub(DateTime.Now, Categories[0], "Subject", 123);

            Costs = new ObservableCollection<ICostViewModel>
            {
                new CostViewModelStub(new DateTime(2000, 1, 1), Categories[1], "Pasta", 10),
                new CostViewModelStub(new DateTime(2000, 1, 1), Categories[1], "Pizza", 15),
                new CostViewModelStub(new DateTime(2000, 1, 22), Categories[1], "Burger", 20),
            };
        }
    }
}
