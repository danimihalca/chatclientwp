#include "WinRTChatClient.hpp"

#include "utils.h"

#include <ChatClient/ChatClient.hpp>

namespace WinRTChat
{ 
	WinRTChatClient::WinRTChatClient() :
	m_chatClient(new ChatClient())
	{
	}

	void WinRTChatClient::setServer(Platform::String^ address, int port)
	{
		m_chatClient->setServer(ToStdString(address), port);
	}
	void WinRTChatClient::login(Platform::String^ userName, Platform::String^ password, int state)
	{
		UserCredentials userCredentials(ToStdString(userName), ToStdString(password));
		m_chatClient->login(userCredentials, static_cast<USER_STATE>(state));
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

	void WinRTChatClient::registerUser(Platform::String^ username, Platform::String^ password, Platform::String^ firstname, Platform::String^ lastname)
	{
		User user(-1, ToStdString(username), ToStdString(password), ToStdString(firstname), ToStdString(lastname));
		m_chatClient->registerUser(user);
	}
	void WinRTChatClient::updateUser(Platform::String^ username, Platform::String^ password, Platform::String^ firstname, Platform::String^ lastname)
	{
		User user(-1, ToStdString(username), ToStdString(password), ToStdString(firstname), ToStdString(lastname));
		m_chatClient->updateUser(user);
	}
	
	void WinRTChatClient::changeState(int state)
	{
		m_chatClient->changeState(static_cast<USER_STATE>(state));
	}


	void WinRTChatClient::addListener(WinRTChatClientListenerWrapper^ rtListener)
	{
		m_chatClient->addListener(rtListener->getListenerImpl());
	}

	void WinRTChatClient::removeListener(WinRTChatClientListenerWrapper^ rtListener)
	{
		m_chatClient->removeListener(rtListener->getListenerImpl());
	}
}