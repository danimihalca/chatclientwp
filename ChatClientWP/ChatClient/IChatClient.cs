﻿using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient
{
    public interface IChatClient
    {
        void SetNotifier(IChatClientNotifier notifier);

        void SetServerProperties(string address, int port);

        void Login(string userName, String password);

        void Disconnect();
        void RequestContacts();

        void SendMessage(Message message);
    }
}
