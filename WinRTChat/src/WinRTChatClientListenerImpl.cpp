#include "WinRTChatClientListenerImpl.hpp"
#include "WinRTContact.hpp"

#include "utils.h"

namespace WinRTChat
{

	WinRTChatClientListenerImpl::WinRTChatClientListenerImpl(WinRTChatClientNotifierDelegate^ notifier) :
		m_notifier(notifier)
	{
	}

	WinRTChatClientListenerImpl::~WinRTChatClientListenerImpl()
	{
	}

	void WinRTChatClientListenerImpl::onConnected()
	{
		if (m_notifier != nullptr && m_notifier->OnConnected != nullptr)
		{
			m_notifier->OnConnected();
		}
	}

	void WinRTChatClientListenerImpl::onDisconnected()
	{
		if (m_notifier != nullptr && m_notifier->OnDisconnected != nullptr)
		{
			m_notifier->OnDisconnected();

		}
	}

	void WinRTChatClientListenerImpl::onMessageReceived(int senderId, const std::string& message)
	{
		if (m_notifier != nullptr && m_notifier->OnMessageReceived != nullptr)
		{
			m_notifier->OnMessageReceived(senderId, ToPlatformString(message));
		}
	}

	void WinRTChatClientListenerImpl::onLoginFailed(const std::string& message)
	{
		if (m_notifier != nullptr && m_notifier->OnLoginFailed != nullptr)
		{
			m_notifier->OnLoginFailed(ToPlatformString(message));
		}
	}

	void WinRTChatClientListenerImpl::onConnectionError()
	{
		if (m_notifier != nullptr && m_notifier->OnConnectionError != nullptr)
		{
			m_notifier->OnConnectionError();
		}
	}

	void WinRTChatClientListenerImpl::onLoginSuccessful()
	{
		if (m_notifier != nullptr && m_notifier->OnLoginSuccessful != nullptr)
		{
			m_notifier->OnLoginSuccessful();
		}
	}

	void WinRTChatClientListenerImpl::onContactsReceived(const Contacts& contacts)
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

	void WinRTChatClientListenerImpl::onContactOnlineStatusChanged(int contactId, bool isOnline)
	{
		if (m_notifier != nullptr && m_notifier->OnContactStatusChanged != nullptr)
		{
			m_notifier->OnContactStatusChanged(contactId, isOnline);
		}
	}
}