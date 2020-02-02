﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels
{
    public class ManageCategoriesViewModel : ViewModelBase, IManageCategoriesViewModel
    {
        public event Action WentBack;
        public event Action Renamed;
        public event Action Added;
        public event Action Deleted;

        public RelayCommand GoBackCommand { get; }
        public RelayCommand ShowRenameCommand { get; }
        public RelayCommand SaveRenameCommand { get; }
        public RelayCommand CancelActionCommand { get; }
        public RelayCommand ShowAddCommand { get; }
        public RelayCommand SaveAddCommand { get; }
        public RelayCommand ShowDeleteCommand { get; }
        public RelayCommand ConfirmDeleteCommand { get; }

        private readonly ICategoriesManager categoriesManager;
        private readonly IMessagesViewModel messagesViewModel;

        private ICostCategoryViewModel selectedCategory;
        public ICostCategoryViewModel SelectedCategory
        {
            get => selectedCategory;
            set { Set(() => SelectedCategory, ref selectedCategory, value); }
        }

        private ObservableCollection<ICostCategoryViewModel> categories;
        public ObservableCollection<ICostCategoryViewModel> Categories
        {
            get => categories;
            set
            {
                // Preserve the selected category after renaming.
                var selectedCategoryId = SelectedCategory?.Id;

                if (Set(() => Categories, ref categories, value))
                {
                    SelectedCategory = Categories?.FirstOrDefault(x => x.Id == selectedCategoryId);
                }
            }
        }

        private ManageCategoriesMenuState menuState;
        public ManageCategoriesMenuState MenuState
        {
            get => menuState;
            private set
            {
                Set(() => MenuState, ref menuState, value);
            }
        }

        private string renamedCategoryNewName;
        public string RenamedCategoryNewName
        {
            get => renamedCategoryNewName;
            set
            {
                Set(() => RenamedCategoryNewName, ref renamedCategoryNewName, value);
            }
        }

        private string newCategoryName;
        public string NewCategoryName
        {
            get => newCategoryName;
            set { Set(() => NewCategoryName, ref newCategoryName, value); }
        }

        public ManageCategoriesViewModel(ICategoriesManager categoriesManager, IMessagesViewModel messagesViewModel)
        {
            this.categoriesManager = categoriesManager;
            this.messagesViewModel = messagesViewModel;

            GoBackCommand = new RelayCommand(GoBack);
            ShowRenameCommand = new RelayCommand(ShowRename);
            SaveRenameCommand = new RelayCommand(SaveRename);
            ShowAddCommand = new RelayCommand(ShowAdd);
            SaveAddCommand = new RelayCommand(SaveAdd);
            ShowDeleteCommand = new RelayCommand(ShowDelete);
            ConfirmDeleteCommand = new RelayCommand(ConfirmDelete);
            CancelActionCommand = new RelayCommand(SetDefaultMenuState);

            MenuState = ManageCategoriesMenuState.DEFAULT;
        }

        private void ShowAdd()
        {
            MenuState = ManageCategoriesMenuState.ADD;
        }

        private void SaveAdd()
        {
            if (string.IsNullOrWhiteSpace(NewCategoryName))
            {
                return;
            }

            var result = categoriesManager.AddCategory(NewCategoryName);

            if (result.IsSuccess)
            {
                NewCategoryName = null;
                SetDefaultMenuState();
                Added?.Invoke();
            }
            else
            {
                messagesViewModel.AddMessage(result.ErrorMessage);
            }
        }

        private void SetDefaultMenuState()
        {
            MenuState = ManageCategoriesMenuState.DEFAULT;
        }

        private void ShowRename()
        {
            MenuState = ManageCategoriesMenuState.RENAME;
        }

        private void SaveRename()
        {
            if (string.IsNullOrWhiteSpace(RenamedCategoryNewName))
            {
                return;
            }

            var result = categoriesManager.RenameCategory(SelectedCategory.Id, RenamedCategoryNewName);

            if (result.IsSuccess)
            {
                RenamedCategoryNewName = null;
                SetDefaultMenuState();
                Renamed?.Invoke();
            }
            else
            {
                messagesViewModel.AddMessage(result.ErrorMessage);
            }
        }

        private void ShowDelete()
        {
            MenuState = ManageCategoriesMenuState.DELETE;
        }

        private void ConfirmDelete()
        {
            if (SelectedCategory == null)
            {
                return;
            }

            var result = categoriesManager.DeleteCategory(SelectedCategory.Id);

            if (result.IsSuccess)
            {
                SelectedCategory = null;
                SetDefaultMenuState();
                Deleted?.Invoke();
            }
            else
            {
                messagesViewModel.AddMessage(result.ErrorMessage);
            }
        }

        private void GoBack()
        {
            SelectedCategory = null;
            WentBack?.Invoke();
        }
    }
}
