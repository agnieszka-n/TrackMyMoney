using GalaSoft.MvvmLight.Command;
using System;
using Models;

namespace ViewModels.Contracts
{
    public interface ICostViewModel
    {
        int Id { get; set; }
        DateTime? Date { get; set; }
        ICostCategoryViewModel Category { get; set; }
        string Subject { get; set; }
        decimal? Amount { get; set; }
        bool IsValid { get; }
        Cost ToModel();
    }
}
