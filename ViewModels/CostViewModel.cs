using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ViewModels.Contracts;

namespace ViewModels
{
    public class CostViewModel : ViewModelBase, ICostViewModel
    {
        private DateTime? date;
        public DateTime? Date
        {
            get => date;
            set { Set(() => Date, ref date, value); }
        }

        private string category;
        public string Category
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
    }
}
