#include "WinRTChatClientListenerImpl.hpp"
#include "WinRTContact.hpp"
#include "WinRTUserDetails.hpp"

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

	void WinRTChatClientListenerImpl::onMessageReceived(const Message& messagee)
	{
		if (m_notifier != nullptr && m_notifier->OnMessageReceived != nullptr)
		{
			m_notifier->OnMessageReceived(messagee.getSenderId(), ToPlatformString(messagee.getMessageText()));
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

	void WinRTChatClientListenerImpl::onLoginSuccessful(const UserDetails& userDetails)
	{
		if (m_notifier != nullptr && m_notifier->OnLoginSuccessful != nullptr)
		{
			WinRTUserDetails^ details = ref new WinRTUserDetails();
			details->id = userDetails.getId();
			details->firstName = ToPlatformString(userDetails.getFirstName());
			details->lastName = ToPlatformString(userDetails.getLastName());
			m_notifier->OnLoginSuccessful(details);
		}
	}

	void WinRTChatClientListenerImpl::onContactsReceived(const std::vector<Contact>& contacts)
	{
		if (m_notifier != nullptr && m_notifier->OnContactsReceived != nullptr)
		{
			//TODO: wrap native contacts to referenced ones
			Platform::Array<WinRTContact^>^ contactArray = ref new Platform::Array<WinRTContact^>(contacts.size());
			int count = 0;
			for (Contact c : contacts)
			{
				contactArray->set(count++, ref new WinRTContact(c.getId(),
																ToPlatformString(c.getUserName()),
																ToPlatformString(c.getFirstName()),
																ToPlatformString(c.getLastName()),
																static_cast<char>(c.getState())));
			}

			m_notifier->OnContactsReceived(contactArray);
		}
	}

	void WinRTChatClientListenerImpl::onContactStateChanged(int contactId, CONTACT_STATE state)
	{
		if (m_notifier != nullptr && m_notifier->OnContactStatusChanged != nullptr)
		{
			m_notifier->OnContactStatusChanged(contactId, static_cast<char>(state));
		}
	}
}