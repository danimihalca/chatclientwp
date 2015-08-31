#include "WinRTChatClientListenerWrapper.hpp"

namespace WinRTChat
{
	WinRTChatClientListenerWrapper::WinRTChatClientListenerWrapper(WinRTChatClientNotifierDelegate^ notifier)
	{
		p_listenerImpl = new WinRTChatClientListener(notifier);
	}

	WinRTChatClientListenerWrapper::~WinRTChatClientListenerWrapper()
	{
		delete p_listenerImpl;
	}

	IChatClientListener* WinRTChatClientListenerWrapper::getListenerImpl()
	{
		return p_listenerImpl;
	}
}