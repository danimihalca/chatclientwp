#pragma once
#include "WinRTContact.hpp"

namespace WinRTChat
{
	public delegate void onConnectedCallback();
	public delegate void onDisconnectedCallback();
	public delegate void onMessageReceivedCallback(int senderId, Platform::String^ message);
	public delegate void onLoginFailedCallback(Platform::String^ message);
	public delegate void onConnectionErrorCallback();
	public delegate void onLoginSuccessfulCallback();
	public delegate void onContactsReceivedCallback(const Platform::Array<WinRTContact^>^ contacts);
	public delegate void onContactStatusChangedCallback(int contactId, bool isOnline);

	public ref class WinRTChatClientNotifierProxy sealed
	{
	public:
		inline WinRTChatClientNotifierProxy()
		{
		}

		property onConnectedCallback^ OnConnected;
		property onDisconnectedCallback^ OnDisconnected;
		property onMessageReceivedCallback^ OnMessageReceived;
		property onLoginFailedCallback^ OnLoginFailed;
		property onConnectionErrorCallback^ OnConnectionError;
		property onLoginSuccessfulCallback^ OnLoginSuccessful;
		property onContactsReceivedCallback^ OnContactsReceived;
		property onContactStatusChangedCallback^ OnContactStatusChanged;
	};
}
