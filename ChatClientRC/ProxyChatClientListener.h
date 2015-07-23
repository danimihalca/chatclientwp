#pragma once

#include <ChatClient\IChatClientListener.h>

//#include <AbstractRTChatClientListener.h>

//ref class AbstractRTChatClientListener;

#include "ChatClientRtC.h">

namespace ChatClientRC
{

	class ProxyChatClientListener:public IChatClientListener
	{
	public:
		ProxyChatClientListener(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m);
		~ProxyChatClientListener();

		void onConnected();
		void onDisconnected();
		void onMessageReceived(const std::string& message);

	private:
		onConnectedCallback^ _c;
		onDisconnectedCallback^ _d;
		onMessageCallback^ _m;
	};
}

