using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.ViewModels
{
    internal class MessagesViewModelStub : IMessagesViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }

        public MessagesViewModelStub()
        {
            var messages = new List<Message>()
            {
                new Message("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur fringilla enim et arcu consequat, in blandit sapien aliquam. Sed vulputate gravida arcu, eget lacinia tortor condimentum commodo. Fusce sollicitudin volutpat quam, non posuere nibh molestie sit amet. Nunc posuere luctus condimentum. Suspendisse potenti. Etiam eu arcu ultricies, pretium felis iaculis, tincidunt orci. Morbi dictum ultrices elementum. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed porttitor, turpis et mattis venenatis, sapien leo placerat eros, at dictum nisl mauris at lorem. ")
            };

            Messages = new ObservableCollection<Message>(messages);
        }

        public void AddMessage(string message)
        { }
    }
}
