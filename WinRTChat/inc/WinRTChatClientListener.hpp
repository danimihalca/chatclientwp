#pragma once

#include "WinRTChatClientListenerImpl.hpp"
#include "WinRTChatClientNotifierDelegate.hpp"

namespace WinRTChat
{

	public ref class WinRTChatClientListener sealed
	{
	public:
		WinRTChatClientListener(WinRTChatClientNotifierDelegate^ notifier);

	internal:
		IChatClientListener* getListenerImpl();

	private:
		~WinRTChatClientListener();

	private:
		IChatClientListener* p_listenerImpl;
	};
}

