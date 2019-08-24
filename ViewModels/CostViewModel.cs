using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Models;
using ViewModels.Contracts;

namespace ViewModels
{
    public class CostViewModel : ViewModelBase, ICostViewModel
    {
        private int id;
        public int Id
        {
            get => id;
            set { Set(() => Id, ref id, value); }
        }

        private DateTime? date;
        public DateTime? Date
        {
            get => date;
            set { Set(() => Date, ref date, value); }
        }

        private ICostCategoryViewModel category;
        public ICostCategoryViewModel Category
        {
            get => category;
            set { Set(() => Category, ref category, value); }
        }

        private string subject;
        public string Subject
        {
            get => subject;
            set { Set(() => Subject, ref subject, value); }
        }

        private decimal? amount;
        public decimal? Amount
        {
            get => amount;
            set { Set(() => Amount, ref amount, value); }
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Subject)
                       && Amount.HasValue
                       && Date.HasValue
                       && Category != null;
            }
        }

        public CostViewModel()
        {
        }

        public CostViewModel(Cost cost, ICostCategoryViewModel category)
        {
            Id = cost.Id;
            Date = cost.Date;
            Subject = cost.Subject;
            Amount = cost.Amount;
            Category = category;
        }

        public Cost ToModel()
        {
            if (!IsValid)
            {
                return null;
            }

            return new Cost()
            {
                Subject = Subject,
                Amount = Amount.Value,
                Date = Date.Value,
                CategoryId = Category.Id
            };
        }
    }
}
