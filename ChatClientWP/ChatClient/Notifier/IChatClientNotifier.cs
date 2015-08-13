using ChatClientWP.ChatClient.Listener;
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

        void NotifyOnConnected();
        void NotifyOnDisconnected();
        void NotifyOnConnectionError();
        void NotifyOnLoginSuccessful(UserDetails userDetails);
        void NotifyOnLoginFailed(AUTHENTICATION_STATUS reason);
        void NotifyOnContactsReceived(IList<Contact> contacts);
        void NotifyOnContactStatusChanged(Contact contact);
        void NotifyOnMessageReceived(Message message);

        void AddLoginListener(ILoginListener listener);

        void RemoveLoginListener(ILoginListener listener);

        void AddUpdateListener(IUpdateListener listener);

        void RemoveRegisterListener(IRegisterListener listener);

        void RemoveUpdateListener(IUpdateListener listener);

        void addRegisterListener(IRegisterListener listener);
    }
}
