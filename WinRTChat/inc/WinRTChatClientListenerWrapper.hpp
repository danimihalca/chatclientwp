#pragma once

#include "WinRTChatClientListener.hpp"
#include "WinRTChatClientNotifierDelegate.hpp"

namespace WinRTChat
{

	public ref class WinRTChatClientListenerWrapper sealed
	{
	public:
		WinRTChatClientListenerWrapper(WinRTChatClientNotifierDelegate^ notifier);

	internal:
		IChatClientListener* getListenerImpl();

	private:
		~WinRTChatClientListenerWrapper();

	private:
		IChatClientListener* p_listenerImpl;
	};
}

