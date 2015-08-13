using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.Listener
{
    public interface IConnectListener: IBaseListener
    {
        void OnConnected();
        void OnConnectionError();
    }

}
