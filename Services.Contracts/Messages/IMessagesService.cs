using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.Services.Contracts.Messages
{
    public interface IMessagesService
    {
        ObservableCollection<Message> Messages { get; }
        void AddMessage(string message);
    }
}
