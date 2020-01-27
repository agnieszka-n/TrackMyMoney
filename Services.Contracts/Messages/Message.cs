using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.Services.Contracts.Messages
{
    public class Message
    {
        public string Text { get; }

        public  Message(string text)
        {
            Text = text;
        }
    }
}
