using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.Listener
{
    public interface IRuntimeListener:IBaseListener
    {
        void OnContactsReceived();

        void OnContactStatusChanged(Contact contact);

        void OnMessageReceived(Message message);

        bool OnAddingByContact(string userName);

        void OnAddContactResponse(string userName, ADD_REQUEST_STATUS status);

        void OnRemovedByContact(Contact contact);
    }
}
