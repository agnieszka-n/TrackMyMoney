using GalaSoft.MvvmLight.Command;
using System;

namespace ViewModels.Contracts
{
    public interface ICostViewModel
    {
        DateTime? Date { get; set; }
        string Category { get; set; }
        string Subject { get; set; }
        decimal? Amount { get; set; }
    }
}
