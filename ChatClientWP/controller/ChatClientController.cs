using System.Collections.Generic;

using ChatClientWP.controller;
using ChatClientWP.Model;
using ChatClientWP.ChatClient;
using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.ChatClient.ChatClientListener;
using ChatClientWP.Repository.ContactRepository;
using ChatClientWP.Repository.MessageRepository;


namespace ChatClientWP
{
    public class ChatClientController : IChatClientController
    {
        private IContactRepository m_contactRepository;
        private IMessageRepository m_messageRepository;
        private IChatClient m_chatClient;
        private IChatClientNotifier m_notifier;
        private User m_user;

        public ChatClientController()
        {
            m_contactRepository = new InMemoryContactRepository();
            m_messageRepository = new InMemoryMessageRepository();
            m_chatClient = new ChatClientWinRTProxy();
            m_notifier = new WinRTChatClientNotifier(this);
            m_chatClient.AddListener(m_notifier);
            m_user = new User();
        }

        public User GetUser()
        {
            return m_user;
        }

        public void Connect(string address, int port)
        {
            m_chatClient.Connect(address, port);
        }
    
        public void Login(string username, string password)
        {
            m_chatClient.Login(username, password);
        }
        
        public void Disconnect()
        {
            m_chatClient.Disconnect();
        }

        public void SendMessage(Message message)
        {
            m_messageRepository.AddMessage(message);

            m_chatClient.SendMessage(message);
        }


        public IList<Contact> GetContacts()
        {
            return m_contactRepository.GetContacts();
        }

        public void RequestContacts()
        {
            m_chatClient.RequestContacts();
        }

        public IList<Message> getMessages(Contact contact)
        {
            return m_messageRepository.GetMessagesWithContact(contact);
        }


        public void SetContacts(IList<Contact> contacts)
        {
            m_contactRepository.SetContacts(contacts);
        }

        public Contact GetContact(int contactId)
        {
            return m_contactRepository.GetContact(contactId);
        }

        public void ClearMessages()
        {
            m_messageRepository.ClearMessages();
        }

        public void ClearContacts()
        {
            m_contactRepository.ClearContacts();
        }

        public void AddRuntimeListener(IRuntimeListener listener)
        {
            m_notifier.AddRuntimeListener(listener);
        }

        public void RemoveRuntimeListener(IRuntimeListener listener)
        {
            m_notifier.RemoveRuntimeListener(listener);
        }

        public void SetLoginListener(ILoginListener listener)
        {
            m_notifier.SetLoginListener(listener);
        }


        public void AddReceivedMessage(Message message)
        {
            m_messageRepository.AddMessage(message);
        }


        public void SetUser(User user)
        {
            m_user = user;
        }
    }
}
