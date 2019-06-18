using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using ViewModelsContracts;

namespace ViewModels
{
    public class CostsListViewModel : ViewModelBase, ICostsListViewModel
    {
        private ObservableCollection<ICostViewModel> costs;
        public ObservableCollection<ICostViewModel> Costs
        {
            get => costs;
            set { Set(() => Costs, ref costs, value); }
        }

        private ICostViewModel newCost;
        public ICostViewModel NewCost
        {
            get => newCost;
            set { Set(() => NewCost, ref newCost, value); }
        }

        private bool isAddingCost;
        public bool IsAddingCost
        {
            get => isAddingCost;
            set { Set(() => IsAddingCost, ref isAddingCost, value); }
        }

        public RelayCommand AddCostCommand { get; }
        public RelayCommand CancelAddingCommand { get; }
        public RelayCommand SaveCostCommand { get; }

        public CostsListViewModel()
        {
            ClearNewCost();
            AddCostCommand = new RelayCommand(AddCost);
            CancelAddingCommand = new RelayCommand(CancelAdding);
            SaveCostCommand = new RelayCommand(SaveCost);

            Costs = new ObservableCollection<ICostViewModel>();
            // TODO use a database 
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

        private void SaveCost()
        {
            // TODO use a database
            Costs.Add(NewCost);
            IsAddingCost = false;
            ClearNewCost();
        }

        private void CancelAdding()
        {
            ClearNewCost();
            IsAddingCost = false;
        }

        private void AddCost()
        {
            IsAddingCost = true;
        }

        private void ClearNewCost()
        {
            NewCost = new CostViewModel();
        }
    }
}
