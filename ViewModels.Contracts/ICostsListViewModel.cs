﻿using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using TrackMyMoney.Common;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface ICostsListViewModel
    {
        IAddCostFormViewModel AddCostFormViewModel { get; }
        ObservableCollection<ICostViewModel> Costs { get; }
        CostsListMenuState MenuState { get; }
        ObservableCollection<ICostCategoryViewModel> Categories { get;}
        RelayCommand ShowAddCostCommand { get; }
        RelayCommand ShowManageCategoriesCommand { get; }
    }
}
