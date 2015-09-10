#include "WinRTChatClient.hpp"

#include "utils.h"

#include <ChatClient/ChatClient.hpp>
#include <debug_utils/log_debug.hpp>

namespace WinRTChat
{ 
	WinRTChatClient::WinRTChatClient() :
	m_chatClient(new ChatClient())
	{
		LOG_DEBUG_METHOD;
	}

	void WinRTChatClient::setServer(Platform::String^ address, int port)
	{
		LOG_DEBUG_METHOD;
		m_chatClient->setServer(ToStdString(address), port);
	}
	void WinRTChatClient::login(Platform::String^ userName, Platform::String^ password, int state)
	{
		LOG_DEBUG_METHOD;
		UserCredentials userCredentials(ToStdString(userName), ToStdString(password));
		m_chatClient->login(userCredentials, static_cast<USER_STATE>(state));
	}

	void WinRTChatClient::sendMessage(int receiverId, Platform::String^ message)
	{
		LOG_DEBUG_METHOD;
		m_chatClient->sendMessage(receiverId, ToStdString(message));
	}

	void WinRTChatClient::disconnect()
	{
		LOG_DEBUG_METHOD;
		m_chatClient->disconnect();
	}
	void WinRTChatClient::requestContacts()
	{
		LOG_DEBUG_METHOD;
		m_chatClient->requestContacts();
	}

	void  WinRTChatClient::addContact(Platform::String^ userName)
	{
		LOG_DEBUG_METHOD;
		m_chatClient->addContact(ToStdString(userName));
	}

	void  WinRTChatClient::removeContact(int contactId)
	{
		LOG_DEBUG_METHOD;
		m_chatClient->removeContact(contactId);
	}

	void WinRTChatClient::registerUser(Platform::String^ username, Platform::String^ password, Platform::String^ firstname, Platform::String^ lastname)
	{
		LOG_DEBUG_METHOD;
		User user(-1, ToStdString(username), ToStdString(password), ToStdString(firstname), ToStdString(lastname));
		m_chatClient->registerUser(user);
	}
	void WinRTChatClient::updateUser(Platform::String^ username, Platform::String^ password, Platform::String^ firstname, Platform::String^ lastname)
	{
		LOG_DEBUG_METHOD;
		User user(-1, ToStdString(username), ToStdString(password), ToStdString(firstname), ToStdString(lastname));
		m_chatClient->updateUser(user);
	}
	
	void WinRTChatClient::changeState(int state)
	{
		LOG_DEBUG_METHOD;
		m_chatClient->changeState(static_cast<USER_STATE>(state));
	}


	void WinRTChatClient::addListener(WinRTChatClientListenerWrapper^ rtListener)
	{
		LOG_DEBUG_METHOD;
		m_chatClient->addListener(rtListener->getListenerImpl());
	}

	void WinRTChatClient::removeListener(WinRTChatClientListenerWrapper^ rtListener)
	{
		LOG_DEBUG_METHOD;
		m_chatClient->removeListener(rtListener->getListenerImpl());
	}
}