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
    internal class CostsListViewModelMock : ICostsListViewModel
    {
        public ObservableCollection<ICostViewModel> Costs { get; set; }
        public ICostViewModel NewCost { get; }
        public bool IsAddingCost { get; }
        public ObservableCollection<ICostCategoryViewModel> Categories { get; }
        public RelayCommand ShowAddCostCommand => null;
        public RelayCommand CancelAddingCommand => null;
        public RelayCommand SaveCostCommand => null;

        public CostsListViewModelMock()
        {
            IsAddingCost = true;
            NewCost = new CostViewModelMock(DateTime.Now, "Category", "Subject", 123);

            Categories = new ObservableCollection<ICostCategoryViewModel>
            {
                new CostCategoryViewModel(new CostCategory() { Id = 1, Name = "Category"})
            };

            Costs = new ObservableCollection<ICostViewModel>
            {
                new CostViewModelMock(new DateTime(2000, 1, 1), "Food", "Pasta", 10),
                new CostViewModelMock(new DateTime(2000, 1, 1), "Food", "Pizza", 15),
                new CostViewModelMock(new DateTime(2000, 1, 2), "Food", "Burger", 20),
                new CostViewModelMock(new DateTime(2000, 1, 31), "Transport", "Bus", 2)
            };
        }
    }
}
