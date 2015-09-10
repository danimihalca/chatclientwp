#include "WinRTChatClientListenerWrapper.hpp"
#include <debug_utils/log_debug.hpp>

namespace WinRTChat
{
	WinRTChatClientListenerWrapper::WinRTChatClientListenerWrapper(WinRTChatClientNotifierDelegate^ notifier)
	{
		LOG_DEBUG_METHOD;
		p_listenerImpl = new WinRTChatClientListener(notifier);
	}

	WinRTChatClientListenerWrapper::~WinRTChatClientListenerWrapper()
	{
		LOG_DEBUG_METHOD;
		delete p_listenerImpl;
	}

	IChatClientListener* WinRTChatClientListenerWrapper::getListenerImpl()
	{
		LOG_DEBUG_METHOD;
		return p_listenerImpl;
	}
}