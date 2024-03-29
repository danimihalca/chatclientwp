﻿using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.ChatClient.Listener
{
    public interface ILoginListener: IConnectListener
    {

        void OnLoginSuccessful(UserDetails userDetails);
        void OnLoginFailed(AUTHENTICATION_STATUS reason);
    }
}
