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
    public class ChatClientNotifier:IChatClientNotifier
    {
        protected IChatClientController m_controller;
        private ILoginListener m_loginListener;
        private IList<IRuntimeListener> m_runtimeListeners;

        public ChatClientNotifier(IChatClientController controller)
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

        public void NotifyOnLoginSuccessful(UserDetails userDetails)
        {
            if (m_loginListener != null)
            {
                m_loginListener.OnLoginSuccessful(userDetails);
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

        public bool NotifyOnAddingByContact(string userName)
        {
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                if (listener.OnAddingByContact(userName))
                {
                    return true;
                }
            }
            return false;
        }

        public void NotifyOnAddContactResponse(string userName, bool accepted)
        {
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnAddContactResponse(userName, accepted);
            }
        }

        public void NotifyOnRemovedByContact(int contactId)
        {
            Contact contact = m_controller.GetContact(contactId);
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnRemovedByContact(contact);
            }
        }

    }
}
