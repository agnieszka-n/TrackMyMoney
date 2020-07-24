using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface IOkCancelDialogWindowViewModel
    {
        string Message { get; }
        RelayCommand OkClickCommand { get; }
        RelayCommand CancelClickCommand { get; }
    }
}
