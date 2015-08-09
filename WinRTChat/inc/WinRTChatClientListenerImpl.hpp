#pragma once

#include <ChatClient\IChatClientListener.hpp>

#include "WinRTChatClientNotifierDelegate.hpp"

namespace WinRTChat
{

	class WinRTChatClientListenerImpl :public IChatClientListener
	{
	public:
		WinRTChatClientListenerImpl(WinRTChatClientNotifierDelegate^ notifier);
		~WinRTChatClientListenerImpl();

		void onConnected();
		void onDisconnected();
		void onMessageReceived(int senderId, const std::string& message);
		void onLoginFailed(const std::string& message);
		void onConnectionError();
		void onLoginSuccessful();
		void onContactsReceived(const Contacts& contacts);
		void onContactOnlineStatusChanged(int contactId, bool isOnline);
	private:
		WinRTChatClientNotifierDelegate^ m_notifier;
	};
}

