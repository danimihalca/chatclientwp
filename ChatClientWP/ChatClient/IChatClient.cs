using ChatClientWP.ChatClient.Notifier;
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
        void AddListener(IChatClientNotifier notifier);
        void RemoveListener(IChatClientNotifier notifier);
        void Connect(string address, int port);

        void Login(string userName, String password, USER_STATE state);

        void Disconnect();
        void RequestContacts();

        void SendMessage(Message message);

        void RemoveContact(int p);

        void AddContact(string userName);

        void RegisterUser(User user);

        void UpdateUser(User user);

        void ChangeState(USER_STATE state);
    }
}
