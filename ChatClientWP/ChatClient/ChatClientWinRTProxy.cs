using WinRTChat;
using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient
{
    class ChatClientWinRTProxy: IChatClient
    {
        private WinRTChatClient m_nativeChatClient;
        
        public ChatClientWinRTProxy()
        {
            m_nativeChatClient = new WinRTChatClient();
        }
        public void AddListener(IChatClientNotifier notifier)
        {
            m_nativeChatClient.addListener((notifier as WinRTChatClientNotifier).GetListener());
        }

        public void RemoveListener(IChatClientNotifier notifier)
        {
            m_nativeChatClient.removeListener((notifier as WinRTChatClientNotifier).GetListener());
        }

        public void SetServerProperties(string address, int port)
        {
            m_nativeChatClient.setServerProperties(address, port);
        }

        public void Login(string userName, string password)
        {
            m_nativeChatClient.login(userName, password);
        }

        public void Disconnect()
        {
            m_nativeChatClient.disconnect();
        }

        public void RequestContacts()
        {
            m_nativeChatClient.requestContacts();
        }

        public void SendMessage(Message message)
        {
            m_nativeChatClient.sendMessage(message.Receiver.Id, message.MessageText);
        }
    }
}
