using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using ViewModelsContracts;

namespace Views.ViewModels
{
    internal class CostsListViewModelMock : ICostsListViewModel
    {
        public ObservableCollection<ICostViewModel> Costs { get; set; }
        public ICostViewModel NewCost { get; }
        public bool IsAddingCost { get; }
        public RelayCommand AddCostCommand => null;
        public RelayCommand CancelAddingCommand => null;
        public RelayCommand SaveCostCommand => null;

        public CostsListViewModelMock()
        {
            IsAddingCost = true;
            NewCost = new CostViewModelMock(DateTime.Now, "Category", "Subject", 123);

            Costs = new ObservableCollection<ICostViewModel>();
            Costs.Add(new CostViewModelMock(new DateTime(2000, 1, 1), "Food", "Pasta", 10));
            Costs.Add(new CostViewModelMock(new DateTime(2000, 1, 1), "Food", "Pizza", 15));
            Costs.Add(new CostViewModelMock(new DateTime(2000, 1, 2), "Food", "Burger", 20));
            Costs.Add(new CostViewModelMock(new DateTime(2000, 1, 31), "Transport", "Bus", 2));
        }
    }
}
