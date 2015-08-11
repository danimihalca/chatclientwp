using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinRTChat;
using ChatClientWP.Model;
using ChatClientWP.ChatClient.ChatClientListener;

namespace ChatClientWP.controller
{
    public interface IChatClientController
    {
        void AddRuntimeListener(IRuntimeListener listener);
        void RemoveRuntimeListener(IRuntimeListener listener);
        void SetLoginListener(ILoginListener listener);

        void RequestContacts();

        void Connect(string address, int port);
        void Login(string userName, String password);

        void Disconnect();

        void SendMessage(Message message);

        void AddReceivedMessage(Message message);

        IList<Message> getMessages(Contact contact);

        IList<Contact> GetContacts();

        void SetContacts(IList<Contact> contacts);
        Contact GetContact(int contactId);

        void ClearMessages();
        void ClearContacts();

        User GetUser();
    }
}
