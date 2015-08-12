#include "WinRTChatClient.hpp"

#include "utils.h"

#include <ChatClient/ChatClient.hpp>

namespace WinRTChat
{ 
	WinRTChatClient::WinRTChatClient() :
	m_chatClient(new ChatClient())
	{
	}

	void WinRTChatClient::connect(Platform::String^ address, int port)
	{
		m_chatClient->connect(ToStdString(address), port);

	}
	void WinRTChatClient::login(Platform::String^ userName, Platform::String^ password)
	{
		UserCredentials userCredentials(ToStdString(userName), ToStdString(password));
		m_chatClient->login(userCredentials);
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
		m_chatClient->requestContacts();
	}

	void  WinRTChatClient::addContact(Platform::String^ userName)
	{
		m_chatClient->addContact(ToStdString(userName));
	}

	void  WinRTChatClient::removeContact(int contactId)
	{
		m_chatClient->removeContact(contactId);
	}

	void WinRTChatClient::addListener(WinRTChatClientListener^ rtListener)
	{
		m_chatClient->addListener(rtListener->getListenerImpl());
	}

	void WinRTChatClient::removeListener(WinRTChatClientListener^ rtListener)
	{
		m_chatClient->removeListener(rtListener->getListenerImpl());
	}
}