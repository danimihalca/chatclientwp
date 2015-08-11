using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.ChatClientListener
{
    public interface ILoginListener: BaseListener
    {
        void OnConnected();
        void OnConnectionError();
        void OnLoginSuccessful(UserDetails userDetails);
        void OnLoginFailed(string reason);
    }
}
