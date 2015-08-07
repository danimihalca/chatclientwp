using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Repository
{
    interface IMessageRepository
    {
        void AddMessage(Message message);
        IList<Message> GetMessagesWithContact(Contact c);
    }
}
