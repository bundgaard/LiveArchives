using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;

namespace LiveArchives.Language
{
    internal interface IMessageProducer 
    {
        public event EventHandler<Message> MessageReceived;
        public void SendMessage(Message message);
    }
}
