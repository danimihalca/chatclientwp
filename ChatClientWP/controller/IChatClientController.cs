using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinRTChat;
using ChatClientWP.Model;
using ChatClientWP.ChatClient.Listener;

namespace ChatClientWP.controller
{
    public interface IChatClientController
    {

        void RequestContacts();

        void SetServer(string address, int port);
        void Login(string userName, String password, USER_STATE state);

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
        void SetUser(User user);

        void AddContact(string userName);

        void RemoveContact(Contact contact, bool notifyServer);

        void AddRegisterListener(IRegisterListener listener);

        void RemoveRegisterListener(IRegisterListener listener);

        void AddUpdateListener(IUpdateListener listener);

        void RemoveUpdateListener(IUpdateListener listener);

        void AddRuntimeListener(IRuntimeListener listener);
        void RemoveRuntimeListener(IRuntimeListener listener);


        void AddLoginListener(ILoginListener listener);

        void RemoveLoginListener(ILoginListener listener);

        void SetConnected(bool connected);

        bool IsConnected();

        USER_STATE GetState();
        void ChangeState(USER_STATE state);

        void RegisterUser(User user);
        void UpdateUser(User user);
    }
}
