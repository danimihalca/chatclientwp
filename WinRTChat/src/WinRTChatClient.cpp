#include "WinRTChatClient.hpp"

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

	void WinRTChatClient::addListener(WinRTChatClientListener^ rtListener)
	{
		std::shared_ptr<IChatClientListener> listener(rtListener->getListenerImpl());
		m_chatClient->addListener(listener);
	}

	void WinRTChatClient::removeListener(WinRTChatClientListener^ rtListener)
	{
		std::shared_ptr<IChatClientListener> listener(rtListener->getListenerImpl());
		m_chatClient->removeListener(listener);
	}
}