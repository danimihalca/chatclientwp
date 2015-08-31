#pragma once

#include <ChatClient/IChatClient.hpp>
#include <memory>

#include "WinRTChatClientListenerWrapper.hpp"

namespace WinRTChat
{
	public ref class WinRTChatClient sealed
	{

	public:
		WinRTChatClient();
		void setServer(Platform::String^ address, int port);
		void login(Platform::String^ userName, Platform::String^ password, int state);
		void sendMessage(int receiverId, Platform::String^ message);
		void addContact(Platform::String^ userName);
		void removeContact(int contactId);
		void disconnect();
		void requestContacts();
		void registerUser(Platform::String^ userName, Platform::String^ password, Platform::String^ firstname, Platform::String^ lastname);
		void updateUser(Platform::String^ userName, Platform::String^ password, Platform::String^ firstname, Platform::String^ lastname);
		void changeState(int state);
		void addListener(WinRTChatClientListenerWrapper^ listener);
		void removeListener(WinRTChatClientListenerWrapper^ listener);

	private:
		std::unique_ptr<IChatClient> m_chatClient;
		IChatClient* a;
	};
}
