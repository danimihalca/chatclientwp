#pragma once

#include <ChatClient/IChatClient.h>
#include <memory>

namespace ChatClientRC
{
	public delegate void onConnectedCallback();
	public delegate void onDisconnectedCallback();
	public delegate void onMessageCallback(Platform::String^ message);

	public ref class ChatClientRtC sealed
	{

	public:
		ChatClientRtC();
		void connect(Platform::String^ address, uint16 port);
		void sendMessage(Platform::String^ message);
		void disconnect();

		void setNotificationCallbacks(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m);

	private:
		std::unique_ptr<IChatClient> m_chatClient;
	};
}
