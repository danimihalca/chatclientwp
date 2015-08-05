#pragma once

#include <ChatClient\IChatClientListener.hpp>

#include "NativeChatClientRC.hpp"

namespace ChatClientRC
{

	class RCChatClientListener :public IChatClientListener
	{
	public:
		RCChatClientListener(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m);
		~RCChatClientListener();

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

