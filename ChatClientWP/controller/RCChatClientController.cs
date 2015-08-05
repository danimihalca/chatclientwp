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
        private ChatClientRtC chatClientRtc;
        private IList<IChatClientListener> listeners;

        public RCChatClientController()
        {
            chatClientRtc = new ChatClientRtC();
            chatClientRtc.setNotificationCallbacks(notifyOnConnected, notifyOnDisconnected, notifyOnMessage);
            listeners = new List<IChatClientListener>();
        }

        public void AddListener(IChatClientListener listener)
        {
            listeners.Add(listener);
        }

        public void SetServerProperties(string address, int port)
        {
            chatClientRtc.setServerProperties(address, port);
        }
    
        public void Login(string username, string password)
        {
            chatClientRtc.login(username, password);
        }
        
        public void Disconnect()
        {
            chatClientRtc.disconnect();
        }

        public void SendMessage(int userId, string message)
        {
            chatClientRtc.sendMessage(userId,message);
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

        private void notifyOnMessage(string message)
        {
            foreach (var listener in listeners)
            {
                listener.OnMessage(message);
            }
        }
    }
}
