using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Utils
{
    public class EnumCodePrettifier
    {
        public static String Prettify(ChatClient.Notifier.REGISTER_UPDATE_USER_STATUS status)
        {
            switch (status)
            {
                case ChatClientWP.ChatClient.Notifier.REGISTER_UPDATE_USER_STATUS.USER_OK:
                    return "Succesfull";
                case ChatClientWP.ChatClient.Notifier.REGISTER_UPDATE_USER_STATUS.USER_EXISTING_USERNAME:
                    return "Username is already used!";
                case ChatClientWP.ChatClient.Notifier.REGISTER_UPDATE_USER_STATUS.USER_INVALID_INPUT:
                    return "Incomplete fields!";
                default:
                    break;
            }
            return "";
        }

        public static String Prettify(ChatClient.Notifier.ADD_REQUEST_STATUS status)
        {
            switch (status)
            {
                case ChatClientWP.ChatClient.Notifier.ADD_REQUEST_STATUS.ADD_ACCEPTED:
                    return "has accepted";
                case ChatClientWP.ChatClient.Notifier.ADD_REQUEST_STATUS.ADD_DECLINED:
                    return "has declined";
                case ChatClientWP.ChatClient.Notifier.ADD_REQUEST_STATUS.ADD_OFFLINE:
                    return "is offline";
                case ChatClientWP.ChatClient.Notifier.ADD_REQUEST_STATUS.ADD_INEXISTENT:
                    return "doens't exist";
                case ChatClientWP.ChatClient.Notifier.ADD_REQUEST_STATUS.ADD_YOURSELF:
                    return "Cannot add yourself";
                default:
                    break;
            }
            return "";
        }

        public static String Prettify(ChatClient.Notifier.AUTHENTICATION_STATUS status)
        {
            switch (status)
            {
                case ChatClientWP.ChatClient.Notifier.AUTHENTICATION_STATUS.AUTH_SUCCESSFUL:
                    return "Login successful!";
                case ChatClientWP.ChatClient.Notifier.AUTHENTICATION_STATUS.AUTH_ALREADY_LOGGED_IN:
                    return "User already logged in!";
                case ChatClientWP.ChatClient.Notifier.AUTHENTICATION_STATUS.AUTH_INVALID_CREDENTIALS:
                    return "Invalid credentials!";
                case ChatClientWP.ChatClient.Notifier.AUTHENTICATION_STATUS.AUTH_INVALID_STATE:
                    return "Invalid state!";
                default:
                    break;
            }
            return "";
        }
    }
}
