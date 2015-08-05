#pragma once

#include <ChatClient\IChatClientListener.hpp>

#include "NativeChatClientRC.hpp"
#include "RCChatClientNotifier.hpp"

namespace ChatClientRC
{

	class RCChatClientListener :public IChatClientListener
	{
	public:
		RCChatClientListener(RCChatClientNotifier^ notifier);
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
		RCChatClientNotifier^ m_notifier;
	};
}

