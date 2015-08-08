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
    public class WinRTChatClientNotifier: IChatClientNotifier
    {
        private IChatClientController m_controller;
        private ILoginListener m_loginListener;
        private IList<IRuntimeListener> m_runtimeListeners;

        public WinRTChatClientNotifier(IChatClientController controller)
        {
            m_controller = controller;
            m_runtimeListeners = new List<IRuntimeListener>();
        }

        public void AddRuntimeListener(IRuntimeListener listener)
        {
            m_runtimeListeners.Add(listener);
        }
        
        public void RemoveRuntimeListener(IRuntimeListener listener)
        {
            m_runtimeListeners.Remove(listener);
        }

        public void SetLoginListener(ILoginListener listener)
        {
            m_loginListener = listener;
        }

        public WinRTChatClientNotifierProxy CreateNotifierProxy()
        {
            WinRTChatClientNotifierProxy m_nativeNotifier = m_nativeNotifier = new WinRTChatClientNotifierProxy();

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

        public void NotifyOnConnected()
        {
            if (m_loginListener != null)
            {
                m_loginListener.OnConnected();
            }
        }

        public void NotifyOnDisconnected()
        {
            m_controller.ClearContacts();
            m_controller.ClearMessages();

            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnDisconnected();
            }

            if (m_loginListener != null)
            {
                m_loginListener.OnDisconnected();
            }
        }

        public void NotifyOnConnectionError()
        {
            if (m_loginListener != null)
            {
                m_loginListener.OnConnectionError();
            }
        }

        public void NotifyOnLoginSuccessful()
        {
            if (m_loginListener != null)
            {
                m_loginListener.OnLoginSuccessful();
            }
        }

        public void NotifyOnLoginFailed(string reason)
        {
            if (m_loginListener != null)
            {
                m_loginListener.OnLoginFailed(reason);
            }
        }

        public void NotifyOnContactsReceived(IList<Contact> contacts)
        {
            m_controller.SetContacts(contacts);
            
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnContactsReceived();
            }
        }

        public void NotifyOnContactStatusChanged(Contact contact)
        {
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnContactStatusChanged(contact);
            }
        }

        public void NotifyOnMessageReceived(Message message)
        {
            Contact contact = message.Sender as Contact;
            contact.UnreadMesssagesCount++;
            m_controller.AddReceivedMessage(message);

            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnMessageReceived(message);
            }
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
