using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatClientRC;
using ChatClientWP.controller;


namespace ChatClientWP
{
    public class RCChatClientController : IChatClientController
    {
        private NativeChatClientRC m_nativeChatClient;
        private IList<IChatClientListener> listeners;

        public RCChatClientController()
        {
            m_nativeChatClient = new NativeChatClientRC();
            listeners = new List<IChatClientListener>();

            RCChatClientNotifier notifier = new RCChatClientNotifier();
            notifier.OnConnected = notifyOnConnected;
            notifier.OnDisconnected = notifyOnDisconnected;
            notifier.OnMessageReceived = notifyOnMessageReceived;
            notifier.OnConnectionError = notifyOnConnectionError;
            notifier.OnContactOnlineStatusChanged = notifyOnContactOnlineStatusChanged;
            notifier.OnLoginSuccessful = notifyOnLoginSuccessful;
            notifier.OnLoginFailed = notifyOnLoginFailed;

            m_nativeChatClient.setNotifier(notifier);

        }



        public void AddListener(IChatClientListener listener)
        {
            listeners.Add(listener);
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
            foreach (var listener in listeners)
            {
                listener.OnLoginSuccessful();
            }
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
    }
}
