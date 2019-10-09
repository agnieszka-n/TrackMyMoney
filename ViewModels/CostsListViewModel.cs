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
            set
            {
                AddCostFormViewModel.Categories = value;
                ManageCategoriesViewModel.Categories = value;
                Set(() => Categories, ref categories, value);
            }
        }

        private ObservableCollection<ICostViewModel> costs;
        public ObservableCollection<ICostViewModel> Costs
        {
            get => costs;
            set { Set(() => Costs, ref costs, value); }
        }

        private IAddCostFormViewModel addCostFormViewModel;
        public IAddCostFormViewModel AddCostFormViewModel
        {
            get => addCostFormViewModel;
            set { Set(() => AddCostFormViewModel, ref addCostFormViewModel, value); }
        }

        private IManageCategoriesViewModel manageCategoriesViewModel;
        public IManageCategoriesViewModel ManageCategoriesViewModel
        {
            get => manageCategoriesViewModel;
            set { Set(() => ManageCategoriesViewModel, ref manageCategoriesViewModel, value); }
        }

        private CostsListMenuState menuState;
        public CostsListMenuState MenuState
        {
            get => menuState;
            set { Set(() => MenuState, ref menuState, value); }
        }

        public RelayCommand ShowAddCostCommand { get; }
        public RelayCommand ShowManageCategoriesCommand { get; }

        public CostsListViewModel(ICategoriesManager categoriesManager, ICostsManager costsManager, IAddCostFormViewModel addCostFormViewModel, IManageCategoriesViewModel manageCategoriesViewModel)
        {
            this.categoriesManager = categoriesManager;
            this.costsManager = costsManager;

            AddCostFormViewModel = addCostFormViewModel;
            AddCostFormViewModel.Saved += LoadCosts;
            AddCostFormViewModel.Cancelled += OnMenuOptionDiscarded;

            ShowAddCostCommand = new RelayCommand(ShowAddCost);
            ShowManageCategoriesCommand = new RelayCommand(ShowManageCategories);

            ManageCategoriesViewModel = manageCategoriesViewModel;
            ManageCategoriesViewModel.WentBack += OnMenuOptionDiscarded;

            LoadCategories();
            LoadCosts();
        }

        private void OnMenuOptionDiscarded()
        {
            MenuState = CostsListMenuState.DEFAULT;
        }

        private void ShowAddCost()
        {
            MenuState = CostsListMenuState.ADD_COST;
        }

        private void ShowManageCategories()
        {
            MenuState = CostsListMenuState.MANAGE_CATEGORIES;
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
    }
}
