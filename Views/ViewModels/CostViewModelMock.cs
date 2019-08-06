using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contracts;

namespace Views.ViewModels
{
    internal class CostViewModelMock : ICostViewModel
    {
        public DateTime? Date { get; set; }
        public string Category { get; set; }
        public string Subject { get; set; }
        public decimal? Amount { get; set; }

        public CostViewModelMock(DateTime date, string category, string subject, decimal amount)
        {
            Date = date;
            Category = category;
            Subject = subject;
            Amount = amount;
        }
    }
}
