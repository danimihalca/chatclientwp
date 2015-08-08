using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.ChatClientListener
{
    public interface BaseListener
    {
        void OnDisconnected();
    }
}
