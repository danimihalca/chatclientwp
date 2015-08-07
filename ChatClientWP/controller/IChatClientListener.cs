using ChatClientWP.Model;
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

        void OnLoginSuccessful();

        void OnLoginFailed(string message);

        void OnContactOnlineStatusChanged(Contact c);

        void OnConnectionError();

        void OnContactsReceived();

        void OnMessageReceived(Message m);
    }
}
