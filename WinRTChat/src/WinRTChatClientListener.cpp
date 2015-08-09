#include "WinRTChatClientListener.hpp"

namespace WinRTChat
{
	WinRTChatClientListener::WinRTChatClientListener(WinRTChatClientNotifierDelegate^ notifier)
	{
		p_listenerImpl = new WinRTChatClientListenerImpl(notifier);
	}

	WinRTChatClientListener::~WinRTChatClientListener()
	{
		delete p_listenerImpl;
	}

	IChatClientListener* WinRTChatClientListener::getListenerImpl()
	{
		return p_listenerImpl;
	}
}