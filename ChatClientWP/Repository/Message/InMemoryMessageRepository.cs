using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Repository.MessageRepository
{
    public class InMemoryMessageRepository: IMessageRepository
    {
        IDictionary<Contact, IList<Message>> m_messages;

        public InMemoryMessageRepository()
        {
            m_messages = new Dictionary<Contact, IList<Message>>();
        }

        public void AddMessage(Message message)
        {
            Contact relatedContact;
            if (message.Sender is Contact)
            {
                relatedContact = message.Sender as Contact;
            }
            else
            {
                relatedContact = message.Receiver as Contact;
            }

            if (!m_messages.ContainsKey(relatedContact))
            {
                m_messages.Add(relatedContact, new List<Message>());
            }
            IList<Message> messagesWithContact;
            m_messages.TryGetValue(relatedContact, out messagesWithContact);
            messagesWithContact.Add(message);
        }

        public IList<Message> GetMessagesWithContact(Contact contact)
        {
            IList<Message> messagesWithContact;
            bool found = m_messages.TryGetValue(contact, out messagesWithContact);
            if (!found)
            {
                messagesWithContact = new List<Message>();
                m_messages.Add(contact, messagesWithContact);
            }
            return messagesWithContact;
        }


        public void ClearMessages()
        {
            m_messages.Clear();
        }
    }
}
