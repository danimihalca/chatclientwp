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
        public WinRTChatClientNotifier(IChatClientController controller):
             base(controller)
        {
        }

        public WinRTChatClientNotifierDelegate CreateNotifierDelegate()
        {
            WinRTChatClientNotifierDelegate m_nativeNotifier = m_nativeNotifier = new WinRTChatClientNotifierDelegate();

            m_nativeNotifier.OnConnected = NotifyOnConnected;
            m_nativeNotifier.OnDisconnected = NotifyOnDisconnected;
            m_nativeNotifier.OnConnectionError = NotifyOnConnectionError;
            m_nativeNotifier.OnLoginSuccessful = NotifyOnLoginSuccessful;
            m_nativeNotifier.OnLoginFailed = NotifyOnLoginFailed;

            m_nativeNotifier.OnContactStatusChanged = NotifyOnContactStatusChangedFromNative;
            m_nativeNotifier.OnMessageReceived = NotifyOnMessageReceivedFromNative;
            m_nativeNotifier.OnContactsReceived = NotifyOnContactsReceivedFromNative;

            return m_nativeNotifier;
        }

        private void NotifyOnContactsReceivedFromNative(WinRTContact[] contacts)
        {
            IList<Contact> contactList = new List<Contact>();
            for (int i = 0; i < contacts.Length; i++)
            {
                Contact c = new Contact();
                c.Id = contacts[i].GetId();
                c.UserName = contacts[i].GetUserName();
                c.FullName = contacts[i].GetFullName();
                c.IsOnline = contacts[i].IsOnline();

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

        private void NotifyOnContactStatusChangedFromNative(int contactId, bool isOnline)
        {
            Contact contact = m_controller.GetContact(contactId);
            contact.IsOnline = isOnline;

            NotifyOnContactStatusChanged(contact);
        }
    }
}
