using ChatClientWP.ChatClient.ChatClientListener;
using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.Notifier
{
    public interface IChatClientNotifier
    {
        void AddRuntimeListener(IRuntimeListener listener);
        void RemoveRuntimeListener(IRuntimeListener listener);
        void SetLoginListener(ILoginListener listener);

        void NotifyOnConnected();
        void NotifyOnDisconnected();
        void NotifyOnConnectionError();
        void NotifyOnLoginSuccessful(UserDetails userDetails);
        void NotifyOnLoginFailed(string reason);
        void NotifyOnContactsReceived(IList<Contact> contacts);
        void NotifyOnContactStatusChanged(Contact contact);
        void NotifyOnMessageReceived(Message message);
    }
}
