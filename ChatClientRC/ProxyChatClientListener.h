#pragma once

#include <ChatClient\IChatClientListener.hpp>

//#include <AbstractRTChatClientListener.h>

//ref class AbstractRTChatClientListener;

#include "ChatClientRtC.h"

namespace ChatClientRC
{

	class ProxyChatClientListener:public IChatClientListener
	{
	public:
		ProxyChatClientListener(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m);
		~ProxyChatClientListener();

		void onConnected();
		void onDisconnected();
		void onMessageReceived(int senderId, const std::string& message);
		void onLoginFailed(const std::string& message);
		void onConnectionError();
		void onLoginSuccessful();
		void onContactsReceived(const Contacts& contacts);
		void onContactOnlineStatusChanged(int contactId, bool isOnline);
	private:
		onConnectedCallback^ _c;
		onDisconnectedCallback^ _d;
		onMessageCallback^ _m;
	};
}

