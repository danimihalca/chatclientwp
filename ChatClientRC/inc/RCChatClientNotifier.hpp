#pragma once
#include "RCContact.hpp"

namespace ChatClientRC
{
	public delegate void onConnectedCallback();
	public delegate void onDisconnectedCallback();
	public delegate void onMessageReceivedCallback(int senderId, Platform::String^ message);
	public delegate void onLoginFailedCallback(Platform::String^ message);
	public delegate void onConnectionErrorCallback();
	public delegate void onLoginSuccessfulCallback();
	public delegate void onContactsReceivedCallback(const Platform::Array<RCContact^>^ contacts);
	public delegate void onContactOnlineStatusChangedCallback(int contactId, bool isOnline);

	public ref class RCChatClientNotifier sealed
	{
	public:
		RCChatClientNotifier();

		property onConnectedCallback^ OnConnected;
		property onDisconnectedCallback^ OnDisconnected;
		property onMessageReceivedCallback^ OnMessageReceived;
		property onLoginFailedCallback^ OnLoginFailed;
		property onConnectionErrorCallback^ OnConnectionError;
		property onLoginSuccessfulCallback^ OnLoginSuccessful;
		property onContactsReceivedCallback^ OnContactsReceived;
		property onContactOnlineStatusChangedCallback^ OnContactOnlineStatusChanged;
	};
}
