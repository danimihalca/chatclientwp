using System.Collections.Generic;

using ChatClientWP.controller;
using ChatClientWP.Model;
using ChatClientWP.ChatClient;
using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.Repository.ContactRepository;
using ChatClientWP.Repository.MessageRepository;
using ChatClientWP.ChatClient.Listener;


namespace ChatClientWP
{
    public class ChatClientController : IChatClientController
    {
        private IContactRepository m_contactRepository;
        private IMessageRepository m_messageRepository;
        private IChatClient m_chatClient;
        private IChatClientNotifier m_notifier;
        private User m_user;
        private USER_STATE m_state;
        private bool m_isConnected;
        public ChatClientController()
        {
            m_contactRepository = new InMemoryContactRepository();
            m_messageRepository = new InMemoryMessageRepository();
            m_chatClient = new ChatClientWinRTProxy();
            m_notifier = new WinRTChatClientNotifier(this);
            m_chatClient.AddListener(m_notifier);
            m_user = new User();
            m_state = USER_STATE.OFFLINE;
            m_isConnected = false;
        }

        public User GetUser()
        {
            return m_user;
        }

        public void SetServer(string address, int port)
        {
            m_chatClient.SetServer(address, port);
        }
    
        public void Login(string username, string password, USER_STATE state)
        {
            m_chatClient.Login(username, password, state);
            m_state = state;
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


        public void AddReceivedMessage(Message message)
        {
            m_messageRepository.AddMessage(message);
        }


        public void SetUser(User user)
        {
            m_user.UserName=user.UserName;
            m_user.FirstName=user.FirstName;
            m_user.LastName=user.LastName;
            m_user.Password=user.Password;
        }


        public void AddContact(string userName)
        {
            m_chatClient.AddContact(userName);
        }

        public void RemoveContact(Contact contact, bool notifyServer)
        {
            if (notifyServer)
            {
                m_chatClient.RemoveContact(contact.Id);
            }
            m_contactRepository.RemoveContact(contact);
            m_messageRepository.RemoveMessages(contact);
        }

        public void AddRegisterListener(IRegisterListener listener)
        {
            m_notifier.addRegisterListener(listener);
        }

        public void RemoveRegisterListener(IRegisterListener listener)
        {
            m_notifier.RemoveRegisterListener(listener);
        }

        public void AddUpdateListener(IUpdateListener listener)
        {
            m_notifier.AddUpdateListener(listener);
        }

        public void RemoveUpdateListener(IUpdateListener listener)
        {
            m_notifier.RemoveUpdateListener(listener);
        }

        public void AddLoginListener(ILoginListener listener)
        {
            m_notifier.AddLoginListener(listener);
        }

        public void RemoveLoginListener(ILoginListener listener)
        {
            m_notifier.RemoveLoginListener(listener);
        }


        public void SetConnected(bool connected)
        {
            m_isConnected = connected;
        }

        public bool IsConnected()
        {
            return m_isConnected;
        }


        public USER_STATE GetState()
        {
            return m_state;
        }

        public void ChangeState(USER_STATE state)
        {
            m_state = state;
            m_chatClient.ChangeState(state);
        }

        public void RegisterUser(User user)
        {
            m_chatClient.RegisterUser(user);
        }

        public void UpdateUser(User user)
        {
            m_chatClient.UpdateUser(user);
        }
    }
}
