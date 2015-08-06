using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP
{
    public interface IChatClientListener
    {
        void OnConnected();
        void OnDisconnected();

        void OnMessageReceived(int senderId, string message);

        void OnLoginSuccessful();

        void OnLoginFailed(string message);

        void OnContactOnlineStatusChanged(int contactId, bool isOnline);

        void OnConnectionError();

        void OnContactsReceived();
    }
}
