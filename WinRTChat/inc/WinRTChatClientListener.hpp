#pragma once

#include <ChatClient\IChatClientListener.hpp>

#include "WinRTChatClientNotifierDelegate.hpp"

namespace WinRTChat
{

	class WinRTChatClientListener :public IChatClientListener
	{
	public:
		WinRTChatClientListener(WinRTChatClientNotifierDelegate^ notifier);
		~WinRTChatClientListener();

		void onDisconnected();
		void onMessageReceived(const Message& message);
		void onLoginFailed(AUTH_STATUS status);
		void onConnectionError();
		void onLoginSuccessful(const UserDetails& userDetails);
		void onContactsReceived(const std::vector<Contact>& contacts);
		void onContactStateChanged(int contactId, USER_STATE state);

		void onRemovedByContact(int contactId);
		void onAddContactResponse(const std::string& userName, ADD_STATUS status);
		bool onAddRequest(const std::string& userName);

		void onRegisterUpdateResponse(REGISTER_UPDATE_USER_STATUS status);

	private:
		WinRTChatClientNotifierDelegate^ m_notifier;
	};
}

