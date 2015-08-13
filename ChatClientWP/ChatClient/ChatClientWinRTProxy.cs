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

        public void Connect(string address, int port)
        {
            m_nativeChatClient.connect(address, port);
        }

        public void Login(string userName, string password, USER_STATE state)
        {
            m_nativeChatClient.login(userName, password, (int) state);
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


        public void RemoveContact(int contactId)
        {
            m_nativeChatClient.removeContact(contactId);
        }

        public void AddContact(string userName)
        {
            m_nativeChatClient.addContact(userName);
        }


        public void RegisterUser(User user)
        {
            m_nativeChatClient.registerUser(user.UserName, user.Password, user.FirstName, user.LastName);
        }

        public void UpdateUser(User user)
        {
            m_nativeChatClient.updateUser(user.UserName, user.Password, user.FirstName, user.LastName);
        }

        public void ChangeState(USER_STATE state)
        {
            m_nativeChatClient.changeState((int)state);
        }
    }
}
