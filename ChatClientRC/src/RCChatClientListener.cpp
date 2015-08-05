#include "RCChatClientListener.hpp"
#include "utils.h"

using namespace ChatClientRC;

RCChatClientListener::RCChatClientListener(RCChatClientNotifier^ notifier) :
m_notifier(notifier)
{
}

RCChatClientListener::~RCChatClientListener()
{
}

void RCChatClientListener::onConnected()
{
	if (m_notifier != nullptr && m_notifier->OnConnected != nullptr)
	{
		m_notifier->OnConnected();
	}
}

void RCChatClientListener::onDisconnected()
{
	if (m_notifier != nullptr && m_notifier->OnDisconnected != nullptr)
	{
		m_notifier->OnDisconnected();

	}
}

void RCChatClientListener::onMessageReceived(int senderId, const std::string& message)
{
	if (m_notifier != nullptr && m_notifier->OnMessageReceived != nullptr)
	{
		m_notifier->OnMessageReceived(senderId, ToPlatformString(message));
	}
}

void RCChatClientListener::onLoginFailed(const std::string& message)
{
	if (m_notifier != nullptr && m_notifier->OnLoginFailed != nullptr)
	{
		m_notifier->OnLoginFailed(ToPlatformString(message));
	}
}

void RCChatClientListener::onConnectionError()
{
	if (m_notifier != nullptr && m_notifier->OnConnectionError != nullptr)
	{
		m_notifier->OnConnectionError();
	}
}

void RCChatClientListener::onLoginSuccessful()
{
	if (m_notifier != nullptr && m_notifier->OnLoginSuccessful != nullptr)
	{
		m_notifier->OnLoginSuccessful();
	}
}

void RCChatClientListener::onContactsReceived(const Contacts& contacts)
{
	//if (m_notifier != nullptr && m_notifier->OnContactsReceived != nullptr)
	//{
	//	//TODO: wrap native contacts to referenced ones
	//	m_notifier->OnContactsReceived();
	//}
}

void RCChatClientListener::onContactOnlineStatusChanged(int contactId, bool isOnline)
{
	if (m_notifier != nullptr && m_notifier->OnContactOnlineStatusChanged != nullptr)
	{
		m_notifier->OnContactOnlineStatusChanged(contactId, isOnline);
	}
}