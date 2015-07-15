#pragma once

#include <chatClientAPI\IChatClient.h>
#include <memory>

namespace ChatClientRC
{
	public ref class ChatClientRtC sealed
	{
	public:
		ChatClientRtC();
		void initialize();
		void connect(Platform::String^ address, uint16 port);
		void startService();
		void sendMessage(Platform::String^ message);
		void closeConnection();

	private:

	private:
		std::unique_ptr<IChatClient> m_chatClient;
	};
}
