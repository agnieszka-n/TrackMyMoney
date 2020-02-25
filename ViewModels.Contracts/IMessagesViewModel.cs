using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface IMessagesViewModel
    {
        ObservableCollection<Message> Messages { get; }
        int? CurrentMessageNumber { get; }
        Message CurrentMessage { get; }

        RelayCommand GoToNextMessageCommand { get; }
        RelayCommand GoToPreviousMessageCommand { get; }
        RelayCommand DismissCurrentMessageCommand { get; }
        RelayCommand DismissSuccessMessagesCommand { get; }

        void AddMessage(string message, MessageTypes type);
    }
}
