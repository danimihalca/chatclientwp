using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatClientRC;
using ChatClientWP.controller;
using System.Diagnostics;


namespace ChatClientWP
{
    public class RCChatClientController : IChatClientController
    {
        private NativeChatClientRC m_nativeChatClient;
        private IList<IChatClientListener> listeners;

        IDictionary<int, Contact> m_contacts;

        public RCChatClientController()
        {
            m_nativeChatClient = new NativeChatClientRC();
            listeners = new List<IChatClientListener>();
            m_contacts = new Dictionary<int, Contact>();
            RCChatClientNotifier notifier = new RCChatClientNotifier();
            notifier.OnConnected = notifyOnConnected;
            notifier.OnDisconnected = notifyOnDisconnected;
            notifier.OnMessageReceived = notifyOnMessageReceived;
            notifier.OnConnectionError = notifyOnConnectionError;
            notifier.OnContactOnlineStatusChanged = notifyOnContactOnlineStatusChanged;
            notifier.OnContactsReceived = notifiyOnContactsReceived;
            notifier.OnLoginSuccessful = notifyOnLoginSuccessful;
            notifier.OnLoginFailed = notifyOnLoginFailed;

            m_nativeChatClient.setNotifier(notifier);

        }

        private void notifiyOnContactsReceived(RCContact[] contacts)
        {
            //throw new NotImplementedException();
            for(int i =0 ; i< contacts.Length; i++)
            {
                Contact c = new Contact() ;
                c.ID = contacts[i].GetId();
                c.UserName = contacts[i].GetUserName();
                c.FullName = contacts[i].GetFullName();
                c.IsOnline= contacts[i].IsOnline();

                m_contacts.Add(c.ID ,c);
            }
            foreach (var listener in listeners)
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
        }

        public void SendMessage(int userId, string message)
        {
            m_nativeChatClient.sendMessage(userId, message);
        }

        private void notifyOnConnected()
        {
            foreach(var listener in listeners)
            {
                listener.OnConnected();
            }
        }

        private void notifyOnDisconnected()
        {
            foreach (var listener in listeners)
            {
                listener.OnDisconnected();
            }
        }

        private void notifyOnMessageReceived(int senderId, string message)
        {
            foreach (var listener in listeners)
            {
                listener.OnMessageReceived(senderId, message);
            }
        }

        private void notifyOnLoginSuccessful()
        {
            IList<IChatClientListener> copy = listeners.ToList<IChatClientListener>();
            foreach (var listener in copy)
            {
                listener.OnLoginSuccessful();
            }
            //lock(listeners)
            //{
            //    foreach (var listener in listeners)
            //    {
            //        listener.OnLoginSuccessful();
            //    }
            //}
            Debug.WriteLine("notifyOnLoginSuccessful");
        }

        private void notifyOnLoginFailed(string message)
        {
            foreach (var listener in listeners)
            {
                listener.OnLoginFailed(message);
            }
        }

        private void notifyOnContactOnlineStatusChanged(int contactId, bool isOnline)
        {
            m_contacts.ElementAt(contactId).Value.IsOnline = isOnline;
            foreach (var listener in listeners)
            {
                listener.OnContactOnlineStatusChanged(contactId, isOnline);
            }
        }

        private void notifyOnConnectionError()
        {
            foreach (var listener in listeners)
            {
                listener.OnConnectionError();
            }
        }


        public IList<Contact> GetContacts()
        {
            return m_contacts.Values.ToList();
        }


        public void RequestContacts()
        {
            m_nativeChatClient.requestContacts();
        }
    }
}
