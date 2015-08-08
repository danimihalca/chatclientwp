#pragma once

#include <ChatClient\IChatClientListener.hpp>

#include "WinRTChatClient.hpp"
#include "WinRTChatClientNotifierProxy.hpp"

namespace WinRTChat
{

	class WinRTChatClientListener :public IChatClientListener
	{
	public:
		WinRTChatClientListener(WinRTChatClientNotifierProxy^ notifier);
		~WinRTChatClientListener();

		void onConnected();
		void onDisconnected();
		void onMessageReceived(int senderId, const std::string& message);
		void onLoginFailed(const std::string& message);
		void onConnectionError();
		void onLoginSuccessful();
		void onContactsReceived(const Contacts& contacts);
		void onContactOnlineStatusChanged(int contactId, bool isOnline);
	private:
		WinRTChatClientNotifierProxy^ m_notifier;
	};
}

