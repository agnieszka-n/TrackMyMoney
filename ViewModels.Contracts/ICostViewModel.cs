using GalaSoft.MvvmLight.Command;
using System;

namespace ViewModels.Contracts
{
    public interface ICostViewModel
    {
        int Id { get; set; }
        DateTime? Date { get; set; }
        ICostCategoryViewModel Category { get; set; }
        string Subject { get; set; }
        decimal? Amount { get; set; }
    }
}
