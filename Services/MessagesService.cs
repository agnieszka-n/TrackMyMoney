using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Services.Contracts.Messages;

namespace TrackMyMoney.Services
{
    public class MessagesService : IMessagesService
    {
        public ObservableCollection<Message> Messages { get; }

        public MessagesService()
        {
            Messages = new ObservableCollection<Message>();
        }

        public void AddMessage(string message)
        {
            Messages.Add(new Message(message));
        }
    }
}
