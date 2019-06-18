﻿using GalaSoft.MvvmLight.Command;
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
        public bool IsDirty { get; set; }
        public bool IsDeleted { get; set; }

        public RelayCommand EditCommand => throw new NotImplementedException();
        public RelayCommand DeleteCommand => throw new NotImplementedException();

        public CostViewModelMock() { }

        public CostViewModelMock(DateTime date, string category, string subject, decimal amount)
        {
            Date = date;
            Category = category;
            Subject = subject;
            Amount = amount;
        }
    }
}
