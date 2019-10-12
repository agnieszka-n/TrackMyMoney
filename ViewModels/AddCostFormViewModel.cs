using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TrackMyMoney.Common;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels
{
    public class AddCostFormViewModel : ViewModelBase, IAddCostFormViewModel
    {
        private readonly ICostsManager costsManager;

        private ICostViewModel newCost;
        public ICostViewModel NewCost
        {
            get => newCost;
            set { Set(() => NewCost, ref newCost, value); }
        }

        private ObservableCollection<ICostCategoryViewModel> categories;
        public ObservableCollection<ICostCategoryViewModel> Categories
        {
            get => categories;
            set { Set(() => Categories, ref categories, value); }
        }

        public event Action Saved;
        public event Action Cancelled;

        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveCommand { get; }

        public AddCostFormViewModel(ICostsManager costsManager)
        {
            this.costsManager = costsManager;

            CancelCommand = new RelayCommand(CancelAddCost);
            SaveCommand = new RelayCommand(SaveCost);

            ClearNewCost();
        }

        private void CancelAddCost()
        {
            ClearNewCost();
            Cancelled?.Invoke();
        }

        private void SaveCost()
        {
            var model = NewCost.ToModel();

            if (model == null)
            {
                return;
            }

            OperationResult result = costsManager.AddCost(model);

            if (result.IsSuccess)
            {
                ClearNewCost();
                Saved?.Invoke();
            }
            // TODO implement an error message 
        }

        private void ClearNewCost()
        {
            NewCost = new CostViewModel();
        }
    }
}
