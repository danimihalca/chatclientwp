using ChatClientWP.ChatClient.Listener;
using ChatClientWP.controller;
using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.Notifier
{
    
    public enum AUTHENTICATION_STATUS
    {
        AUTH_SUCCESSFUL ,
        AUTH_ALREADY_LOGGED_IN,
        AUTH_INVALID_CREDENTIALS,
        AUTH_INVALID_STATE
    }
    public enum ADD_REQUEST_STATUS
    {
        ADD_ACCEPTED,
        ADD_DECLINED,
        ADD_OFFLINE,
        ADD_INEXISTENT,
        ADD_YOURSELF
    };


    public enum REGISTER_UPDATE_USER_STATUS
    {
        USER_OK,
        USER_EXISTING_USERNAME ,
        USER_INVALID_INPUT
    };

    public class ChatClientNotifier:IChatClientNotifier
    {
        protected IChatClientController m_controller;
        private IList<IRuntimeListener> m_runtimeListeners;
        private IList<IConnectListener> m_connectListeners;
        private IList<ILoginListener> m_loginListeners;
        private IList<IRegisterUpdateListener> m_registerUpdateListeners;

        public ChatClientNotifier(IChatClientController controller)
        {
            m_controller = controller;
            m_runtimeListeners = new List<IRuntimeListener>();
            m_connectListeners = new List<IConnectListener>();
            m_loginListeners = new List<ILoginListener>();
            m_registerUpdateListeners = new List<IRegisterUpdateListener>();
        }

        public void AddRuntimeListener(IRuntimeListener listener)
        {
            m_runtimeListeners.Add(listener);
        }
        
        public void RemoveRuntimeListener(IRuntimeListener listener)
        {
            m_runtimeListeners.Remove(listener);
        }


        //public void NotifyOnConnected()
        //{
        //    List<IConnectListener> reverseList = m_connectListeners.ToList<IConnectListener>();
        //    reverseList.Reverse();
        //    foreach (IConnectListener listener in reverseList)
        //    {
        //        listener.OnConnected();
        //    }
        //}

        public void NotifyOnDisconnected()
        {
            m_controller.SetConnected(false);
            m_controller.ClearContacts();
            m_controller.ClearMessages();

            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnDisconnected();
            }

            List<IConnectListener> reverseConnectList = m_connectListeners.ToList<IConnectListener>();
            reverseConnectList.Reverse();
            foreach (IConnectListener listener in reverseConnectList)
            {
                listener.OnDisconnected();
            }
        }

        public void NotifyOnConnectionError()
        {
            List<IConnectListener> reverseConnectList = m_connectListeners.ToList<IConnectListener>();
            reverseConnectList.Reverse();
            foreach (IConnectListener listener in reverseConnectList)
            {
                listener.OnConnectionError();
            }
        }

        public void NotifyOnLoginSuccessful(UserDetails userDetails)
        {
            List<ILoginListener> reverseList = m_loginListeners.ToList<ILoginListener>();
            reverseList.Reverse();
            foreach (ILoginListener listener in reverseList)
            {
                listener.OnLoginSuccessful(userDetails);
            }

        }

        public void NotifyOnLoginFailed(AUTHENTICATION_STATUS reason)
        {
            //if (m_loginListener != null)
            //{
            //    m_loginListener.OnLoginFailed(reason);
            //}

            List<ILoginListener> reverseList = m_loginListeners.ToList<ILoginListener>();
            reverseList.Reverse();
            foreach (ILoginListener listener in reverseList)
            {
                listener.OnLoginFailed(reason);
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

        public void NotifyOnContactStateChanged(Contact contact)
        {
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnContactStateChanged(contact);
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

        public bool NotifyOnAddRequest(string userName)
        {
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                if (listener.OnAddRequest(userName))
                {
                    return true;
                }
            }
            return false;
        }

        public void NotifyOnAddContactResponse(string userName, ADD_REQUEST_STATUS status)
        {
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnAddContactResponse(userName, status);
            }
        }

        public void NotifyOnRemovedByContact(int contactId)
        {
            Contact contact = m_controller.GetContact(contactId);
            m_controller.RemoveContact(contact, false);
            List<IRuntimeListener> reverseList = m_runtimeListeners.ToList<IRuntimeListener>();
            reverseList.Reverse();
            foreach (IRuntimeListener listener in reverseList)
            {
                listener.OnRemovedByContact(contact);
            }
        }

        public void NotifyOnRegisterUpdate(REGISTER_UPDATE_USER_STATUS status)
        {
            List<IRegisterUpdateListener> reverseList = m_registerUpdateListeners.ToList<IRegisterUpdateListener>();
            reverseList.Reverse();
            foreach (IRegisterUpdateListener listener in reverseList)
            {
                listener.onRegisterUpdateResponse(status);
            }
        }

        public void AddLoginListener(ILoginListener listener)
        {
            m_loginListeners.Add(listener);
            m_connectListeners.Add(listener);
        }

        public void RemoveLoginListener(ILoginListener listener)
        {
            m_loginListeners.Remove(listener);
            m_connectListeners.Remove(listener);
        }

        public void AddUpdateListener(IUpdateListener listener)
        {
            m_runtimeListeners.Add(listener);
            m_registerUpdateListeners.Add(listener);
        }

        public void RemoveRegisterListener(IRegisterListener listener)
        {
            m_registerUpdateListeners.Remove(listener);
            m_connectListeners.Remove(listener);
        }

        public void RemoveUpdateListener(IUpdateListener listener)
        {
            m_runtimeListeners.Remove(listener);
            m_registerUpdateListeners.Remove(listener);
        }

        public void addRegisterListener(IRegisterListener listener)
        {
            m_registerUpdateListeners.Add(listener);
            m_connectListeners.Add(listener);
        }
    }
}
