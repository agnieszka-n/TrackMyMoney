﻿using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace ViewModelsContracts
{
    public interface ICostsListViewModel
    {
        ObservableCollection<ICostViewModel> Costs { get; }
        ICostViewModel NewCost { get; }
        bool IsAddingCost { get; }
        RelayCommand AddCostCommand { get; }
        RelayCommand CancelAddingCommand { get; }
        RelayCommand SaveCostCommand { get; }
    }
}
