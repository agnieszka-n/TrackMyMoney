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
    }
}
