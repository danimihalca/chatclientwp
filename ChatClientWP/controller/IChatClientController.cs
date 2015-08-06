using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatClientRC;

namespace ChatClientWP.controller
{
    public interface IChatClientController
    {
        void AddListener(IChatClientListener listener);
        void RemoveListener(IChatClientListener listener);
        void RequestContacts();

        void SetServerProperties(string address, int port);
        void Login(string userName, String password);

        void Disconnect();

        void SendMessage(int userId, string message);

        IList<Contact> GetContacts();
    }
}
