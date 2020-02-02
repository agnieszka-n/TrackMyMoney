using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels
{
    public class MessagesViewModel : ViewModelBase, IMessagesViewModel
    {
        public ObservableCollection<Message> Messages { get; }

        public MessagesViewModel()
        {
            Messages = new ObservableCollection<Message>();
        }

        public void AddMessage(string message)
        {
            Messages.Add(new Message(message));
        }
    }
}
