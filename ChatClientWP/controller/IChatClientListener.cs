using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP
{
    public interface IChatClientListener
    {
        void OnConnected();
        void OnDisconnected();
        void OnMessage(string message);
    }
}
