#include "WinRTChatClientListener.hpp"
#include "WinRTContact.hpp"

#include "utils.h"

namespace WinRTChat
{

	WinRTChatClientListener::WinRTChatClientListener(WinRTChatClientNotifierDelegate^ notifier) :
		m_notifier(notifier)
	{
	}

	WinRTChatClientListener::~WinRTChatClientListener()
	{
	}

	void WinRTChatClientListener::onConnected()
	{
		if (m_notifier != nullptr && m_notifier->OnConnected != nullptr)
		{
			m_notifier->OnConnected();
		}
	}

	void WinRTChatClientListener::onDisconnected()
	{
		if (m_notifier != nullptr && m_notifier->OnDisconnected != nullptr)
		{
			m_notifier->OnDisconnected();

		}
	}

	void WinRTChatClientListener::onMessageReceived(int senderId, const std::string& message)
	{
		if (m_notifier != nullptr && m_notifier->OnMessageReceived != nullptr)
		{
			m_notifier->OnMessageReceived(senderId, ToPlatformString(message));
		}
	}

	void WinRTChatClientListener::onLoginFailed(const std::string& message)
	{
		if (m_notifier != nullptr && m_notifier->OnLoginFailed != nullptr)
		{
			m_notifier->OnLoginFailed(ToPlatformString(message));
		}
	}

	void WinRTChatClientListener::onConnectionError()
	{
		if (m_notifier != nullptr && m_notifier->OnConnectionError != nullptr)
		{
			m_notifier->OnConnectionError();
		}
	}

	void WinRTChatClientListener::onLoginSuccessful()
	{
		if (m_notifier != nullptr && m_notifier->OnLoginSuccessful != nullptr)
		{
			m_notifier->OnLoginSuccessful();
		}
	}

	void WinRTChatClientListener::onContactsReceived(const Contacts& contacts)
	{
		if (m_notifier != nullptr && m_notifier->OnContactsReceived != nullptr)
		{
			//TODO: wrap native contacts to referenced ones
			Platform::Array<WinRTContact^>^ contactArray = ref new Platform::Array<WinRTContact^>(contacts.size());
			int count = 0;
			for (Contact c : contacts)
			{
				contactArray->set(count++, ref new WinRTContact(c.getDetails().getId(),
																ToPlatformString(c.getUserName()),
																ToPlatformString(c.getDetails().getFullName()),
																c.isOnline()));
			}

			m_notifier->OnContactsReceived(contactArray);
		}
	}

	void WinRTChatClientListener::onContactOnlineStatusChanged(int contactId, bool isOnline)
	{
		if (m_notifier != nullptr && m_notifier->OnContactStatusChanged != nullptr)
		{
			m_notifier->OnContactStatusChanged(contactId, isOnline);
		}
	}
}