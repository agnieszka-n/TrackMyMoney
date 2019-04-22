using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsContracts;

namespace ViewModels
{
    public class CostsListViewModel : ViewModelBase, ICostsListViewModel
    {
        private ObservableCollection<ICostViewModel> costs;
        public ObservableCollection<ICostViewModel> Costs
        {
            get { return costs; }
            set { Set(() => Costs, ref costs, value); }
        }

        public CostsListViewModel()
        {
            Costs = new ObservableCollection<ICostViewModel>();
            Costs.Add(new CostViewModel()
            {
                Date = new DateTime(2000, 2, 2),
                Category = "Entertainment",
                Subject = "Cinema",
                Amount = 15
            });
            Costs.Add(new CostViewModel()
            {
                Date = new DateTime(2000, 2, 3),
                Category = "Entertainment",
                Subject = "Theater",
                Amount = 30
            });
            Costs.Add(new CostViewModel()
            {
                Date = new DateTime(2000, 2, 4),
                Category = "Entertainment",
                Subject = "Opera",
                Amount = 45
            });
        }
    }
}
