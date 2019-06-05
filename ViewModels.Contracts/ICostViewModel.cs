using GalaSoft.MvvmLight.Command;
using System;

namespace ViewModelsContracts
{
    public interface ICostViewModel
    {
        DateTime? Date { get; set; }
        string Category { get; set; }
        string Subject { get; set; }
        decimal? Amount { get; set; }
        bool IsDirty { get; set; }
        bool IsDeleted { get; set; }

        RelayCommand EditCommand { get; }
        RelayCommand DeleteCommand { get; }
    }
}