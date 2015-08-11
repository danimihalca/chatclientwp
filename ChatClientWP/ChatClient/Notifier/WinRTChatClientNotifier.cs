using WinRTChat;
using ChatClientWP.ChatClient.ChatClientListener;
using ChatClientWP.controller;
using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.Notifier
{
    public class WinRTChatClientNotifier : ChatClientNotifier
    {
        private WinRTChatClientListener m_nativeListenerWrapper;

        public WinRTChatClientNotifier(IChatClientController controller):
             base(controller)
        {
            WinRTChatClientNotifierDelegate notifierDelegate = CreateNotifierDelegate();

            m_nativeListenerWrapper = new WinRTChatClientListener(notifierDelegate);
        }

        public WinRTChatClientListener GetListener()
        {
            return m_nativeListenerWrapper;
        }

        private WinRTChatClientNotifierDelegate CreateNotifierDelegate()
        {
            WinRTChatClientNotifierDelegate notifierDelegate = new WinRTChatClientNotifierDelegate();

            notifierDelegate.OnConnected = NotifyOnConnected;
            notifierDelegate.OnDisconnected = NotifyOnDisconnected;
            notifierDelegate.OnConnectionError = NotifyOnConnectionError;
            notifierDelegate.OnLoginSuccessful = NotifyOnLoginSuccessfulFromNative;
            notifierDelegate.OnLoginFailed = NotifyOnLoginFailed;

            notifierDelegate.OnContactStatusChanged = NotifyOnContactStatusChangedFromNative;
            notifierDelegate.OnMessageReceived = NotifyOnMessageReceivedFromNative;
            notifierDelegate.OnContactsReceived = NotifyOnContactsReceivedFromNative;

            return notifierDelegate;
        }

        private void NotifyOnLoginSuccessfulFromNative(WinRTUserDetails details)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.Id = details.id;
            userDetails.FirstName = details.firstName;
            userDetails.LastName = details.lastName;
            NotifyOnLoginSuccessful(userDetails);
        }

        private void NotifyOnContactsReceivedFromNative(WinRTContact[] contacts)
        {
            IList<Contact> contactList = new List<Contact>();
            for (int i = 0; i < contacts.Length; i++)
            {
                Contact c = new Contact();
                c.Id = contacts[i].id;
                c.UserName = contacts[i].userName;
                c.FirstName = contacts[i].firstName;
                c.LastName = contacts[i].lastName;
                c.State = (ContactState) contacts[i].state;

                contactList.Add(c);
            }
            NotifyOnContactsReceived(contactList);
        }

        private void NotifyOnMessageReceivedFromNative(int senderId, string message)
        {
            Message m = new Message();
            m.Sender = m_controller.GetContact(senderId);
            m.Receiver = m_controller.GetUser();
            m.MessageText = message;

            NotifyOnMessageReceived(m);
        }

        private void NotifyOnContactStatusChangedFromNative(int contactId, byte state)
        {
            Contact contact = m_controller.GetContact(contactId);
            contact.State = (ContactState) state;

            NotifyOnContactStatusChanged(contact);
        }
    }
}
