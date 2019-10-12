using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Models;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels
{
    public class CostCategoryViewModel : ViewModelBase, ICostCategoryViewModel
    {
        private int id;
        public int Id
        {
            get => id;
            set { Set(() => Id, ref id, value); }
        }

        private string name;
        public string Name
        {
            get => name;
            set { Set(() => Name, ref name, value); }
        }

        public CostCategoryViewModel() { }

        public CostCategoryViewModel(CostCategory category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}
