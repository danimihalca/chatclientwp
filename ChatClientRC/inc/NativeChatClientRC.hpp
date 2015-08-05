#pragma once

#include <ChatClient/IChatClient.hpp>
#include <memory>

#include "RCChatClientNotifier.hpp"

namespace ChatClientRC
{


	public ref class NativeChatClientRC sealed
	{

	public:
		NativeChatClientRC();
		void setServerProperties(Platform::String^ address, int port);
		void login(Platform::String^ userName, Platform::String^ password);
		void sendMessage(int receiverId, Platform::String^ message);
		void disconnect();

		void setNotifier(RCChatClientNotifier^ notifier);

	private:
		std::unique_ptr<IChatClient> m_chatClient;
	};
}
