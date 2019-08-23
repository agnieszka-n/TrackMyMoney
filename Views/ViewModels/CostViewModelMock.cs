using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ViewModels;
using ViewModels.Contracts;

namespace Views.ViewModels
{
    internal class CostViewModelMock : ICostViewModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public ICostCategoryViewModel Category { get; set; }
        public string Subject { get; set; }
        public decimal? Amount { get; set; }

        public CostViewModelMock(DateTime date, ICostCategoryViewModel category, string subject, decimal amount)
        {
            Date = date;
            Category = category;
            Subject = subject;
            Amount = amount;
        }
    }
}
