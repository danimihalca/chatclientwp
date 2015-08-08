#pragma once

#include <ChatClient/IChatClient.hpp>
#include <memory>

#include "WinRTChatClientNotifierProxy.hpp"

namespace WinRTChat
{
	public ref class WinRTChatClient sealed
	{

	public:
		WinRTChatClient();
		void setServerProperties(Platform::String^ address, int port);
		void login(Platform::String^ userName, Platform::String^ password);
		void sendMessage(int receiverId, Platform::String^ message);
		void disconnect();
		void requestContacts();

		void setNotifier(WinRTChatClientNotifierProxy^ notifier);

	private:
		std::unique_ptr<IChatClient> m_chatClient;
	};
}
