using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.ViewModels.Contracts
{
    public class Message
    {
        public string Text { get; }
        public MessageTypes Type { get; }

        public Message(string text, MessageTypes type)
        {
            Text = text;
            Type = type;
        }
    }

    public enum MessageTypes
    {
        SUCCESS,
        ERROR
    }
}
