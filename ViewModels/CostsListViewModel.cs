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

        private CostsListMenuState menuState;
        public CostsListMenuState MenuState
        {
            get => menuState;
            private set { Set(() => MenuState, ref menuState, value); }
        }

        private IMessagesViewModel messagesViewModel;
        public IMessagesViewModel MessagesViewModel
        {
            get => messagesViewModel;
            set { Set(() => MessagesViewModel, ref messagesViewModel, value); }
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

        public RelayCommand ShowAddCostCommand { get; }
        public RelayCommand ShowManageCategoriesCommand { get; }

        public CostsListViewModel(ICategoriesManager categoriesManager, ICostsManager costsManager, IMessagesViewModel messagesViewModel, IAddCostFormViewModel addCostFormViewModel, IManageCategoriesViewModel manageCategoriesViewModel)
        {
            this.categoriesManager = categoriesManager;
            this.costsManager = costsManager;
            MessagesViewModel = messagesViewModel;

            AddCostFormViewModel = addCostFormViewModel;
            AddCostFormViewModel.Saved += LoadCosts;
            AddCostFormViewModel.Cancelled += SetDefaultMenuState;

            ShowAddCostCommand = new RelayCommand(ShowAddCost);
            ShowManageCategoriesCommand = new RelayCommand(ShowManageCategories);

            ManageCategoriesViewModel = manageCategoriesViewModel;
            ManageCategoriesViewModel.WentBack += SetDefaultMenuState;
            ManageCategoriesViewModel.Renamed += ReloadCostCategories;
            ManageCategoriesViewModel.Added += ReloadCostCategories;
            ManageCategoriesViewModel.Deleted += ReloadCostCategories;

            LoadCategories();
            LoadCosts();

            SetDefaultMenuState();
        }

        private void SetDefaultMenuState()
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

        private void ReloadCostCategories()
        {
            LoadCategories();
            SetCategoryForCosts();
        }

        private void LoadCategories()
        {
            OperationResult<List<CostCategory>> categoriesResult = categoriesManager.GetCategories();
            if (categoriesResult.IsSuccess)
            {
                var categoryViewModels = categoriesResult.Data.Select(x => new CostCategoryViewModel(x));
                Categories = new ObservableCollection<ICostCategoryViewModel>(categoryViewModels);
            }
            else
            {
                messagesViewModel.AddMessage(categoriesResult.ErrorMessage);
            }
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
                var costsViewModels = costsResult.Data.Select(x =>
                    new CostViewModel(x, Categories.Single(category => category.Id == x.CategoryId)));
                Costs = new ObservableCollection<ICostViewModel>(costsViewModels);
            }
            else
            {
                messagesViewModel.AddMessage(costsResult.ErrorMessage);
            }
        }

        private void SetCategoryForCosts()
        {
            foreach (var cost in Costs)
            {
                cost.Category = Categories.Single(category => category.Id == cost.Category.Id);
            }
        }
    }
}
