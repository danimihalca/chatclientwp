#include "WinRTChatClientListener.hpp"
#include "WinRTContact.hpp"
#include "WinRTUserDetails.hpp"

#include "utils.h"
#include <debug_utils/log_debug.hpp>

namespace WinRTChat
{

	WinRTChatClientListener::WinRTChatClientListener(WinRTChatClientNotifierDelegate^ notifier) :
		m_notifier(notifier)
	{
		LOG_DEBUG_METHOD;
	}

	WinRTChatClientListener::~WinRTChatClientListener()
	{
		LOG_DEBUG_METHOD;
	}

	void WinRTChatClientListener::onDisconnected()
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnDisconnected != nullptr)
		{
			m_notifier->OnDisconnected();

		}
	}

	void WinRTChatClientListener::onMessageReceived(const Message& messagee)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnMessageReceived != nullptr)
		{
			m_notifier->OnMessageReceived(messagee.getSenderId(), ToPlatformString(messagee.getMessageText()));
		}
	}

	void WinRTChatClientListener::onLoginFailed(AUTH_STATUS status)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnLoginFailed != nullptr)
		{
			m_notifier->OnLoginFailed(status);
		}
	}

	void WinRTChatClientListener::onConnectionError()
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnConnectionError != nullptr)
		{
			m_notifier->OnConnectionError();
		}
	}

	void WinRTChatClientListener::onLoginSuccessful(const UserDetails& userDetails)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnLoginSuccessful != nullptr)
		{
			WinRTUserDetails^ details = ref new WinRTUserDetails();
			details->id = userDetails.getId();
			details->firstName = ToPlatformString(userDetails.getFirstName());
			details->lastName = ToPlatformString(userDetails.getLastName());
			m_notifier->OnLoginSuccessful(details);
		}
	}

	void WinRTChatClientListener::onContactsReceived(const std::vector<Contact>& contacts)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnContactsReceived != nullptr)
		{
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

	void WinRTChatClientListener::onContactStateChanged(int contactId, USER_STATE state)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnContactStateChanged != nullptr)
		{
			m_notifier->OnContactStateChanged(contactId, static_cast<char>(state));
		}
	}


	void WinRTChatClientListener::onRemovedByContact(int contactId)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnRemovedByContact != nullptr)
		{
			m_notifier->OnRemovedByContact(contactId);
		}
	}

	void WinRTChatClientListener::onAddContactResponse(const std::string& userName, ADD_STATUS status)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnRemovedByContact != nullptr)
		{
			m_notifier->OnAddContactResponse(ToPlatformString(userName), status);
		}
	}

	bool WinRTChatClientListener::onAddRequest(const std::string& userName)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnAddRequest != nullptr)
		{
			return m_notifier->OnAddRequest(ToPlatformString(userName));
		}
		return false;
	}

	void WinRTChatClientListener::onRegisterUpdateResponse(REGISTER_UPDATE_USER_STATUS status)
	{
		LOG_DEBUG_METHOD;
		if (m_notifier != nullptr && m_notifier->OnRegisterUpdateResponse != nullptr)
		{
			m_notifier->OnRegisterUpdateResponse(status);
		}
	}

}