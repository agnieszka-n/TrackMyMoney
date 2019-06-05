using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ViewModelsContracts;

namespace ViewModels
{
    public class CostViewModel : ViewModelBase, ICostViewModel
    {
        private DateTime? date;
        public DateTime? Date
        {
            get { return date; }
            set { Set(() => Date, ref date, value); }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { Set(() => Category, ref category, value); }
        }

        private string subject;
        public string Subject
        {
            get { return subject; }
            set { Set(() => Subject, ref subject, value); }
        }

        private decimal? amount;
        public decimal? Amount
        {
            get { return amount; }
            set { Set(() => Amount, ref amount, value); }
        }

        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
            set { Set(() => IsDirty, ref isDirty, value); }
        }

        private bool isDeleted;
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { Set(() => IsDeleted, ref isDeleted, value); }
        }

        public RelayCommand EditCommand => throw new NotImplementedException();
        public RelayCommand DeleteCommand => throw new NotImplementedException();
    }
}
