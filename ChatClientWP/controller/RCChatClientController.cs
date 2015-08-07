using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatClientRC;
using ChatClientWP.controller;
using System.Diagnostics;
using ChatClientWP.Repository;
using ChatClientWP.Model;


namespace ChatClientWP
{
    public class RCChatClientController : IChatClientController
    {
        private NativeChatClientRC m_nativeChatClient;
        private IList<IChatClientListener> listeners;

        IContactRepository  m_contactRepository;
        IMessageRepository m_messageRepository;

        private ClientInstanceUser m_user;

        public RCChatClientController()
        {
            m_nativeChatClient = new NativeChatClientRC();
            listeners = new List<IChatClientListener>();
            m_contactRepository = new InMemoryContactRepository();
            m_messageRepository = new InMemoryMessageRepository();

            m_user = new ClientInstanceUser();
            m_user.Id = 9999;
            m_user.FullName="ME";
            m_user.UserName = "me";

            RCChatClientNotifier notifier = new RCChatClientNotifier();
            notifier.OnConnected = notifyOnConnected;
            notifier.OnDisconnected = notifyOnDisconnected;
            notifier.OnMessageReceived = notifyOnMessageReceived;
            notifier.OnConnectionError = notifyOnConnectionError;
            notifier.OnContactOnlineStatusChanged = notifyOnContactOnlineStatusChanged;
            notifier.OnContactsReceived = notifyOnContactsReceived;
            notifier.OnLoginSuccessful = notifyOnLoginSuccessful;
            notifier.OnLoginFailed = notifyOnLoginFailed;

            m_nativeChatClient.setNotifier(notifier);

        }

        public ClientInstanceUser GetUser()
        {
            return m_user;
        }

        private void notifyOnContactsReceived(RCContact[] contacts)
        {
            //throw new NotImplementedException();
            for(int i =0 ; i< contacts.Length; i++)
            {
                Contact c = new Contact() ;
                c.Id = contacts[i].GetId();
                c.UserName = contacts[i].GetUserName();
                c.FullName = contacts[i].GetFullName();
                c.IsOnline= contacts[i].IsOnline();

                m_contactRepository.AddContact(c);
            }
            List<IChatClientListener> reverseList = listeners.ToList<IChatClientListener>();
            reverseList.Reverse();
            foreach (var listener in reverseList)
            {
                listener.OnContactsReceived();
            }
        }



        public void AddListener(IChatClientListener listener)
        {
            listeners.Add(listener);
        }
        public void RemoveListener(IChatClientListener listener)
        {
            listeners.Remove(listener);
        }
        public void SetServerProperties(string address, int port)
        {
            m_nativeChatClient.setServerProperties(address, port);
        }
    
        public void Login(string username, string password)
        {
            m_nativeChatClient.login(username, password);
        }
        
        public void Disconnect()
        {
            m_nativeChatClient.disconnect();
            listeners.Clear();
        }

        public void SendMessage(Message message)
        {
            m_messageRepository.AddMessage(message);

            m_nativeChatClient.sendMessage(message.Receiver.Id, message.MessageText);
        }

        private void notifyOnConnected()
        {
            List<IChatClientListener> reverseList = listeners.ToList<IChatClientListener>();
            reverseList.Reverse();
            foreach (var listener in reverseList)
            {
                listener.OnConnected();
            }
        }

        private void notifyOnDisconnected()
        {
            List<IChatClientListener> reverseList = listeners.ToList<IChatClientListener>();
            reverseList.Reverse();
            foreach (var listener in reverseList)
            {
                listener.OnDisconnected();
            }
            listeners.Clear();
            m_messageRepository.ClearMessages();
            m_contactRepository.ClearContacts();
        }

        private void notifyOnMessageReceived(int senderId, string message)
        {
            Contact c  = m_contactRepository.FindContact(senderId);
            c.UnreadMesssagesCount++;

            Message m = new Message();
            m.Sender = c;
            m.Receiver = m_user;
            m.MessageText = message;
            m_messageRepository.AddMessage(m);

            List<IChatClientListener> reverseList = listeners.ToList<IChatClientListener>();
            reverseList.Reverse();
            foreach (var listener in reverseList)
            {
                listener.OnMessageReceived(m);
            }
        }

        private void notifyOnLoginSuccessful()
        {
            List<IChatClientListener> reverseList = listeners.ToList<IChatClientListener>();
            reverseList.Reverse();
            foreach (var listener in reverseList)
            {
                listener.OnLoginSuccessful();
            }
            Debug.WriteLine("notifyOnLoginSuccessful");
        }

        private void notifyOnLoginFailed(string message)
        {
            List<IChatClientListener> reverseList = listeners.ToList<IChatClientListener>();
            reverseList.Reverse();
            foreach (var listener in reverseList)
            {
                listener.OnLoginFailed(message);
            }
        }

        private void notifyOnContactOnlineStatusChanged(int contactId, bool isOnline)
        {
            Contact c = m_contactRepository.FindContact(contactId);
            c.IsOnline = isOnline;
            List<IChatClientListener> reverseList = listeners.ToList<IChatClientListener>();
            reverseList.Reverse();
            foreach (var listener in reverseList)
            {
                listener.OnContactOnlineStatusChanged(c);
            }
        }

        private void notifyOnConnectionError()
        {
            List<IChatClientListener> reverseList = listeners.ToList<IChatClientListener>();
            reverseList.Reverse();
            foreach (var listener in reverseList)
            {
                listener.OnConnectionError();
            }
            listeners.Clear();
            m_messageRepository.ClearMessages();
            m_contactRepository.ClearContacts();
        }


        public IList<Contact> GetContacts()
        {
            return m_contactRepository.GetContacts();
        }


        public void RequestContacts()
        {
            m_nativeChatClient.requestContacts();
        }


        public IList<Message> getMessages(int contactId)
        {
            Contact c = m_contactRepository.FindContact(contactId);
            return m_messageRepository.GetMessagesWithContact(c);
        }
    }
}
