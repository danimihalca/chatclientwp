#pragma once

#include <ChatClient/IChatClient.hpp>
#include <memory>

#include "WinRTChatClientListener.hpp"

namespace WinRTChat
{
	public ref class WinRTChatClient sealed
	{

	public:
		WinRTChatClient();
		void connect(Platform::String^ address, int port);
		void login(Platform::String^ userName, Platform::String^ password);
		void sendMessage(int receiverId, Platform::String^ message);
		void addContact(Platform::String^ userName);
		void removeContact(int contactId);
		void disconnect();
		void requestContacts();

		void addListener(WinRTChatClientListener^ listener);
		void removeListener(WinRTChatClientListener^ listener);

	private:
		std::unique_ptr<IChatClient> m_chatClient;
		IChatClient* a;
	};
}