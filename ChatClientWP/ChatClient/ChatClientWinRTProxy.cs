﻿using ChatClientRC;
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
        private NativeChatClientRC m_nativeChatClient;
        
        public ChatClientWinRTProxy()
        {
            m_nativeChatClient = new NativeChatClientRC();
        }
        public void SetNotifier(IChatClientNotifier notifier)
        {
            m_nativeChatClient.setNotifier((notifier as WinRTChatClientNotifier).GetNativeNotifier());
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