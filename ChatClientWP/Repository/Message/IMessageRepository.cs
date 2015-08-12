using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Repository.MessageRepository
{
    interface IMessageRepository
    {
        void AddMessage(Message message);
        IList<Message> GetMessagesWithContact(Contact c);

        void ClearMessages();

        void RemoveMessages(Contact contact);
    }
}
