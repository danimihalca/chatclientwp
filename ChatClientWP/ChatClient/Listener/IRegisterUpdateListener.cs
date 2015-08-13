using ChatClientWP.ChatClient.Notifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.Listener
{
    public interface IRegisterUpdateListener
    {
        void onRegisterUpdateResponse(REGISTER_UPDATE_USER_STATUS status);
    }
}
