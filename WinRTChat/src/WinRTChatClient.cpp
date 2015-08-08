#include "WinRTChatClient.hpp"
#include "WinRTChatClientListener.hpp"

#include "utils.h"

#include <ChatClient/ChatClient.hpp>

namespace WinRTChat
{ 
	WinRTChatClient::WinRTChatClient() :
	m_chatClient(new ChatClient())
	{
	}

	void WinRTChatClient::setServerProperties(Platform::String^ address, int port)
	{
		m_chatClient->setServerProperties(ToStdString(address), port);

	}
	void WinRTChatClient::login(Platform::String^ userName, Platform::String^ password)
	{
		m_chatClient->login(ToStdString(userName), ToStdString(password));
	}

	void WinRTChatClient::sendMessage(int receiverId, Platform::String^ message)
	{
		m_chatClient->sendMessage(receiverId, ToStdString(message));
	}

	void WinRTChatClient::disconnect()
	{
		m_chatClient->disconnect();
	}
	void WinRTChatClient::requestContacts()
	{
		m_chatClient->getContacts();
	}


	void WinRTChatClient::setNotifier(WinRTChatClientNotifierDelegate^ notifier)
	{
		std::shared_ptr<IChatClientListener> listener(new WinRTChatClientListener(notifier));
		m_chatClient->addListener(listener);
	}
}