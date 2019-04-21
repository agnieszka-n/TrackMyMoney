using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsContracts;

namespace Main.ViewModels
{
    internal class CostsListViewModel : ICostsListViewModel
    {
        public ObservableCollection<ICostViewModel> Costs { get; set; }

        public CostsListViewModel()
        {
            Costs = new ObservableCollection<ICostViewModel>();
            Costs.Add(new CostViewModel(new DateTime(2000, 1, 1), "Food", "Pasta", 10));
            Costs.Add(new CostViewModel(new DateTime(2000, 1, 1), "Food", "Pizza", 15));
            Costs.Add(new CostViewModel(new DateTime(2000, 1, 2), "Food", "Burger", 20));
            Costs.Add(new CostViewModel(new DateTime(2000, 1, 31), "Transport", "Bus", 2));
        }
    }
}
