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
		void onLoginFailed(const std::string& message);
		void onConnectionError();
		void onLoginSuccessfull();
	private:
		onConnectedCallback^ _c;
		onDisconnectedCallback^ _d;
		onMessageCallback^ _m;
	};
}

