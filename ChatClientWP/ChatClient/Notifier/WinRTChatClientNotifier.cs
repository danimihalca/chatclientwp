using WinRTChat;
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
        private WinRTChatClientListenerWrapper m_nativeListenerWrapper;

        public WinRTChatClientNotifier(IChatClientController controller):
             base(controller)
        {
            WinRTChatClientNotifierDelegate notifierDelegate = CreateNotifierDelegate();

            m_nativeListenerWrapper = new WinRTChatClientListenerWrapper(notifierDelegate);
        }

        public WinRTChatClientListenerWrapper GetListener()
        {
            return m_nativeListenerWrapper;
        }

        private WinRTChatClientNotifierDelegate CreateNotifierDelegate()
        {
            WinRTChatClientNotifierDelegate notifierDelegate = new WinRTChatClientNotifierDelegate();

            //notifierDelegate.OnConnected = NotifyOnConnected;
            notifierDelegate.OnDisconnected = NotifyOnDisconnected;
            notifierDelegate.OnConnectionError = NotifyOnConnectionError;
            notifierDelegate.OnLoginSuccessful = NotifyOnLoginSuccessfulFromNative;
            notifierDelegate.OnLoginFailed = NotifyOnLoginFailed;

            notifierDelegate.OnContactStateChanged = NotifyOnContactStateChangedFromNative;
            notifierDelegate.OnMessageReceived = NotifyOnMessageReceivedFromNative;
            notifierDelegate.OnContactsReceived = NotifyOnContactsReceivedFromNative;

            notifierDelegate.OnRemovedByContact = NotifyOnRemovedByContact;
		    notifierDelegate.OnAddContactResponse = NotifyOnAddContactResponseFromNative;
            notifierDelegate.OnAddRequest = NotifyOnAddRequest;

            notifierDelegate.OnRegisterUpdateResponse = NotifyOnRegisterUpdateFromNative;
            return notifierDelegate;
        }

        private void NotifyOnLoginFailed(byte reason)
        {
            NotifyOnLoginFailed((AUTHENTICATION_STATUS) reason);
        }

        private void NotifyOnAddContactResponseFromNative(string username, byte status)
        {
            NotifyOnAddContactResponse(username, (ADD_REQUEST_STATUS)status);
        }

        private void NotifyOnRegisterUpdateFromNative(byte status)
        {
            NotifyOnRegisterUpdate((REGISTER_UPDATE_USER_STATUS)status);
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
                c.State = (USER_STATE)contacts[i].state;

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

        private void NotifyOnContactStateChangedFromNative(int contactId, byte state)
        {
            Contact contact = m_controller.GetContact(contactId);
            contact.State = (USER_STATE)state;

            NotifyOnContactStateChanged(contact);
        }
    }
}
