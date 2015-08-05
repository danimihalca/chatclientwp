#pragma once

#include <ChatClient/IChatClient.hpp>
#include <memory>

namespace ChatClientRC
{
	public delegate void onConnectedCallback();
	public delegate void onDisconnectedCallback();
	public delegate void onMessageCallback(Platform::String^ message);

	public ref class NativeChatClientRC sealed
	{

	public:
		NativeChatClientRC();
		void setServerProperties(Platform::String^ address, int port);
		void login(Platform::String^ userName, Platform::String^ password);
		void sendMessage(int receiverId, Platform::String^ message);
		void disconnect();

		void setNotificationCallbacks(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m);

	private:
		std::unique_ptr<IChatClient> m_chatClient;
	};
}
