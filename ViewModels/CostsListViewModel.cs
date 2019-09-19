using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Common;
using GalaSoft.MvvmLight.Command;
using TrackMyMoney.Models;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels
{
    public class CostsListViewModel : ViewModelBase, ICostsListViewModel
    {
        private readonly ICategoriesManager categoriesManager;
        private readonly ICostsManager costsManager;

        private ObservableCollection<ICostCategoryViewModel> categories;
        public ObservableCollection<ICostCategoryViewModel> Categories
        {
            get => categories;
            set { Set(() => Categories, ref categories, value); }
        }

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

        public RelayCommand ShowAddCostCommand { get; }
        public RelayCommand CancelAddingCommand { get; }
        public RelayCommand SaveCostCommand { get; }

        public CostsListViewModel(ICategoriesManager categoriesManager, ICostsManager costsManager)
        {
            this.categoriesManager = categoriesManager;
            this.costsManager = costsManager;

            ClearNewCost();

            ShowAddCostCommand = new RelayCommand(ShowAddCost);
            CancelAddingCommand = new RelayCommand(CancelAdding);
            SaveCostCommand = new RelayCommand(SaveCost);

            LoadCategories();
            LoadCosts();
        }

        private void SaveCost()
        {
            var model = NewCost.ToModel();

            if (model == null)
            {
                return;
            }

            OperationResult result = costsManager.SaveCost(model);

            if (result.IsSuccess)
            {
                ClearNewCost();
                LoadCosts();
            }
            // TODO implement an error message 
        }

        private void CancelAdding()
        {
            ClearNewCost();
            IsAddingCost = false;
        }

        private void ShowAddCost()
        {
            IsAddingCost = true;
        }

        private void LoadCategories()
        {
            OperationResult<List<CostCategory>> categoriesResult = categoriesManager.GetCategories();
            if (categoriesResult.IsSuccess)
            {
                var categoryViewModels = categoriesResult.Data.Select(x => new CostCategoryViewModel(x));
                Categories = new ObservableCollection<ICostCategoryViewModel>(categoryViewModels);
            }
            // TODO implement an error message 
        }

        private void LoadCosts()
        {
            if (Categories == null)
            {
                return;
            }

            OperationResult<List<Cost>> costsResult = costsManager.GetCosts();
            if (costsResult.IsSuccess)
            {
                var costsViewModels = costsResult.Data.Select(x => new CostViewModel(x, Categories.Single(category => category.Id == x.CategoryId)));
                Costs = new ObservableCollection<ICostViewModel>(costsViewModels);
            }
            // TODO implement an error message 
        }

        private void ClearNewCost()
        {
            NewCost = new CostViewModel();
        }
    }
}
