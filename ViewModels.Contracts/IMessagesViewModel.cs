using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.ViewModels.Contracts
{
    public interface IMessagesViewModel
    {
        ObservableCollection<Message> Messages { get; }
        void AddMessage(string message);
    }
}
