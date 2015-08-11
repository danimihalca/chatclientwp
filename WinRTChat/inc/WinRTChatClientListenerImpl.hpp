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
		void onMessageReceived(const Message& message);
		void onLoginFailed(const std::string& message);
		void onConnectionError();
		void onLoginSuccessful(const UserDetails& userDetails);
		void onContactsReceived(const std::vector<Contact>& contacts);
		void onContactStateChanged(int contactId, CONTACT_STATE state);
	private:
		WinRTChatClientNotifierDelegate^ m_notifier;
	};
}

