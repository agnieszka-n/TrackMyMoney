using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using TrackMyMoney.Services.Contracts.Dialogs;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.ViewModels
{
    internal class OkCancelDialogWindowViewModelStub : IOkCancelDialogWindowViewModel
    {
        public string Message { get; }
        public RelayCommand OkClickCommand => null;
        public RelayCommand CancelClickCommand => null;

        public OkCancelDialogWindowViewModelStub()
        {
            Message = "Are you sure you want to delete it?";
        }
    }
}
