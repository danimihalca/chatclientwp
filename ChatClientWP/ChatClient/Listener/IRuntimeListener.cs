using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.ChatClientListener
{
    public interface IRuntimeListener:BaseListener
    {
        void OnContactsReceived();

        void OnContactStatusChanged(Contact contact);

        void OnMessageReceived(Message message);

        bool OnAddingByContact(string userName);

        void OnAddContactResponse(string userName, bool accepted);

        void OnRemovedByContact(Contact contact);
    }
}
