using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatClientRC;


namespace ChatClientWP
{
    public class ChatClient
    {
        private ChatClientRtC chatClientRtc;
        private IList<IChatClientListener> listeners;

        public ChatClient()
        {
            chatClientRtc = new ChatClientRtC();
            chatClientRtc.setNotificationCallbacks(notifyOnConnected, notifyOnDisconnected, notifyOnMessage);
            listeners = new List<IChatClientListener>();
        }

        public void addListener(IChatClientListener listener)
        {
            listeners.Add(listener);
        }

        public void connect(string address, UInt16 port)
        {
            chatClientRtc.connect(address, port);
        }

        public void disconnect()
        {
            chatClientRtc.disconnect();
        }

        public void sendMessage(string message)
        {
            chatClientRtc.sendMessage(message);
        }

        private void notifyOnConnected()
        {
            foreach(var listener in listeners)
            {
                listener.onConnected();
            }
        }

        private void notifyOnDisconnected()
        {
            foreach (var listener in listeners)
            {
                listener.onDisconnected();
            }
        }

        private void notifyOnMessage(string message)
        {
            foreach (var listener in listeners)
            {
                listener.onMessage(message);
            }
        }
    }
}
